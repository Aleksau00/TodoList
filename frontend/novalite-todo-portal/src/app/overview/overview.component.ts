import { Component } from '@angular/core';
import {TodoListModel} from "../model/todoList.model";
import {Observable} from "rxjs";
import {TodoListService} from "../services/todoList.service";
import {CommonModule, NgForOf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {
  MatCard,
  MatCardActions,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardTitle
} from "@angular/material/card";
import {MatPaginator} from "@angular/material/paginator";
import {Router} from "@angular/router";

@Component({
  selector: 'app-overview',
  standalone: true,
  imports: [HttpClientModule, NgForOf, MatCard, MatCardHeader, MatCardContent, MatCardActions, MatPaginator, MatCardTitle, MatCardFooter],
  providers: [TodoListService],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})
export class OverviewComponent {

  lists: TodoListModel[] = [];
  constructor(private todoListService: TodoListService, private router: Router) {

  }

  ngOnInit() : void {
    this.getAll();
  }

  getAll() : void {
    this.todoListService.getAll().subscribe(res => {
      this.lists = res;
      console.log(this.lists)
    });
  }

  goToDetails(list : TodoListModel) {
    this.router.navigate(['/lists/'+ list.id]);

  }
}
