import {Component, NgModule, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {TodoListModel} from "../model/todoList.model";
import {TodoListService} from "../services/todoList.service";
import {ReactiveFormsModule} from "@angular/forms";
import {FormsModule} from "@angular/forms";
import {NgModel} from "@angular/forms";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {CommonModule} from "@angular/common";
import {MatButton} from "@angular/material/button";
import {CreateListRequestModel} from "../details/DTOs/createListRequest.model";

@Component({
  selector: 'app-new',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatFormField,
    MatCardHeader,
    MatCardContent,
    MatCard,
    MatInput,
    MatLabel,
    CommonModule,
    MatButton
  ],
  templateUrl: './new.component.html',
  styleUrl: './new.component.css'
})
export class NewComponent {


  todoListModel : CreateListRequestModel = new CreateListRequestModel();
  constructor(private route: ActivatedRoute, private todoListService: TodoListService, private router: Router) {
  }

  ngOnInit() : void {

  }

  saveChanges(todoListModel: any) : void {
    console.log(todoListModel)
    this.todoListService.createList(todoListModel).subscribe( res => {
      console.log(res);
    })

  }

  goToOverview() {
    this.router.navigate(['/lists'])
  }
}
