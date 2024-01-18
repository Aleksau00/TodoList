import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {TodoListModel} from "../model/todoList.model";


@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  private baseUrl = 'https://localhost:7232/api/TodoList';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAll(): Observable<TodoListModel[]> {
    return this.http.get<TodoListModel[]>(this.baseUrl, {headers: this.headers});
  }


}
