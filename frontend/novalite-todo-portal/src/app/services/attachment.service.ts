import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TodoAttachmentService {
  private baseUrl = 'http://localhost:7232/api/attachments';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAttachments(id: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`, { headers: this.headers });
  }
}
