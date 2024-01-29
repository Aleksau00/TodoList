import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {TodoListModel} from "../model/todoList.model";
import {TodoListAndItemsDTOModel} from "../model/todoListAndItemsDTO.model";
import {TodoItemModel} from "../model/todoItem.model";


@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  private baseUrl = 'http://localhost:7232/api/lists';
  private reminderUrl = 'http://localhost:7232/api/reminders'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAll(): Observable<TodoListModel[]> {
    return this.http.get<TodoListModel[]>(this.baseUrl, {headers: this.headers});
  }

  getList(id : any): Observable<TodoListAndItemsDTOModel> {
    return this.http.get<TodoListAndItemsDTOModel>(this.baseUrl+"/"+id, {headers: this.headers});
  }

  updateList(todoListModel : any) : Observable<any> {
    return this.http.put<any>(this.baseUrl, todoListModel, {headers: this.headers})
  }

  createList(todoList : any) : Observable<any> {
    return this.http.post<any>(this.baseUrl, todoList, {headers: this.headers})
  }

  createItem(todoItem: any, todoListId: string) : Observable<any> {
    const todoItemDTO = {
      content: todoItem,
      todoListId: todoListId,
      status: 0
    }
    console.log(todoItemDTO)
    return this.http.post<any>(this.baseUrl+"/item", todoItemDTO, {headers: this.headers})
  }

  createReminder(id: string) : Observable<any> {
    const todoRequestModel = {
      todoListId: id
    }
    return this.http.post<any>(this.reminderUrl, todoRequestModel, {headers: this.headers})
  }

  editItem(todoItem : TodoItemModel) : Observable<any> {
    const todoItemDTO = {
      id: todoItem.id,
      content: todoItem.content,
      status: Number(todoItem.status)
    }
    console.log(todoItemDTO);
    console.log(this.baseUrl+"/item")
    return this.http.put<any>(this.baseUrl+"/item", todoItemDTO, {headers: this.headers})
  }

}
