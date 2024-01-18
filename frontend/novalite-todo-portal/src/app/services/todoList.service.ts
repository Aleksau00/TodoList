import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {TodoListModel} from "../model/todoList.model";


@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  private baseUrl = 'http://localhost:7232/api/lists';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAll(): Observable<TodoListModel[]> {
    return this.http.get<TodoListModel[]>(this.baseUrl, {headers: this.headers});
  }

  getList(id : any): Observable<TodoListModel> {
    return this.http.get<TodoListModel>(this.baseUrl+"/"+id, {headers: this.headers});
  }

  updateList(todoListModel : any) : Observable<any> {
    return this.http.put<any>(this.baseUrl, todoListModel, {headers: this.headers})
  }

}
