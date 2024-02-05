import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {TodoListModel} from "../model/todoList.model";
import {TodoListAndItemsDTOModel} from "../model/todoListAndItemsDTO.model";
import {TodoItemModel} from "../model/todoItem.model";
import {MsalService} from "@azure/msal-angular";
import {UserResponseModel} from "../model/UserResponse.model";


@Injectable({
  providedIn: 'root'
})
export class TodoUserService {
  private baseUrl = 'http://localhost:7232/api/admin';
  private headers = new HttpHeaders({
    Authorization: `Bearer ${this.authService.instance.getActiveAccount()?.idToken}`,
    'Content-Type': 'application/json',  // Add other headers as needed
    'Role': this.getRole()
  });

  constructor(private http: HttpClient, private authService: MsalService) { }

  getAll(): Observable<UserResponseModel[]> {
    return this.http.get<UserResponseModel[]>(this.baseUrl, {headers: this.headers});
  }

  update(userResponseModel : any) : Observable<any> {
    return this.http.put<any>(this.baseUrl, userResponseModel, {headers: this.headers})
  }

  getRole() : any {
    return localStorage.getItem('Role') ?? 'defaultValue';
  }

}
