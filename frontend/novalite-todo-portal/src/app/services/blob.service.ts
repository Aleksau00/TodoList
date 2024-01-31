import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from "rxjs";
import {TodoListAndItemsDTOModel} from "../model/todoListAndItemsDTO.model";
import {SasDtoModel} from "../details/DTOs/SasDto.model";
import {BlobServiceClient, ContainerClient} from "@azure/storage-blob";
import { saveAs } from 'file-saver';
import {AttachmentsDTOModel} from "../details/DTOs/AttachmentsDTO.model";

@Injectable({
  providedIn: 'root'
})
export class BlobStorageService {

  private baseUrl = 'http://localhost:7232/api/attachments/';
  private readonly blobUrl = "https://aleksau00.blob.core.windows.net/attachments?";
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  private containerClient(sas?: string) : ContainerClient {
    let token = "";
    if (sas) {
      token = sas;
    }
    return new BlobServiceClient(`https://aleksau00.blob.core.windows.net?${token}`)
      .getContainerClient("attachments");
  }


  constructor(private http: HttpClient) { }

  getSas(blobName : string, todoListId : string): Observable<SasDtoModel> {
    const options = {
      headers: this.headers,
      responseType: 'json' as 'json',  // Specify responseType as 'text'
    };

    return this.http.get<SasDtoModel>(this.baseUrl + 'getSas/' + blobName + '/' + todoListId, options);
  }

  getReadSas(blobName : string): Observable<SasDtoModel> {
    const options = {
      headers: this.headers,
      responseType: 'json' as 'json',  // Specify responseType as 'text'
    };

    return this.http.get<SasDtoModel>(this.baseUrl + 'getSas/' + blobName, options);
  }

  uploadFile(sas: string, content: Blob, name: string) : void {
    const blockBlobClient = this.containerClient(sas).getBlockBlobClient(name);
    blockBlobClient
      .uploadData(content, {blobHTTPHeaders: {blobContentType: content.type}}).then(r => alert("Successfully uploaded"))
  }

  downloadBlob(attachment: AttachmentsDTOModel, sas: string) {
    const blobClient = this.containerClient(sas).getBlobClient(attachment.id);
    blobClient.download().then(resp => {
      resp.blobBody?.then(blob => {
        saveAs(blob, attachment.fileName);
      });
    });
  }

}
