import { Component } from '@angular/core';
import {TodoListModel} from "../model/todoList.model";
import {Observable} from "rxjs";
import {TodoListService} from "../services/todoList.service";
import {CommonModule, NgForOf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";

@Component({
  selector: 'app-overview',
  standalone: true,
  imports: [HttpClientModule, NgForOf],
  providers: [TodoListService],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})
export class OverviewComponent {

  lists: TodoListModel[] = [];
  constructor(private todoListService: TodoListService) {

  }

  ngOnInit() : void {
    console.log(this.getAll());
  }

  getAll() : void {
    this.todoListService.getAll().subscribe(res => {
      this.lists = res;
      console.log(res);
    });
  }

}
