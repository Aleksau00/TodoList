import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {TodoListService} from "../services/todoList.service";
import {TodoItemModel} from "../model/todoItem.model";
import {BlobStorageService} from "../services/blob.service";
import {AttachmentsDTOModel} from "../details/DTOs/AttachmentsDTO.model";
import {MatCard, MatCardHeader, MatCardTitle} from "@angular/material/card";
import {CommonModule} from "@angular/common";
import {MatIcon} from "@angular/material/icon";
import {MatButton} from "@angular/material/button";
import {response} from "express";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-attachment-list',
  standalone: true,
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    CommonModule,
    MatIcon,
    MatButton
  ],
  templateUrl: './attachment-list.component.html',
  styleUrl: './attachment-list.component.css'
})
export class AttachmentListComponent {
  attachmentsData: AttachmentsDTOModel[] = [];

  constructor(public dialogRef: MatDialogRef<AttachmentListComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private router: Router, private blobService: BlobStorageService) {
    this.attachmentsData = data.attachments;
  }

  ngOnInit(): void {
    console.log(this.attachmentsData)
  }

  downloadFile(attachment: AttachmentsDTOModel) : void {
    try {
      this.blobService.getReadSas(attachment.id).subscribe((response => {
        const sas = response.sas;
        this.blobService.downloadBlob(attachment, sas);
        console.log("Success")
      }))

    } catch(error) {
      alert(error)
    }
  }

}
