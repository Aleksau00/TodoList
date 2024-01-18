import {Component, NgModule, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {TodoListModel} from "../model/todoList.model";
import {TodoListService} from "../services/todoList.service";
import {ReactiveFormsModule} from "@angular/forms";
import {FormsModule} from "@angular/forms";
import {NgModel} from "@angular/forms";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [
    MatFormField,
    ReactiveFormsModule,
    FormsModule,
    MatCardHeader,
    MatCardContent,
    MatCard,
    MatInput,
    MatLabel,
    CommonModule,
  ],
  providers: [NgModel],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {


  todoListModel : TodoListModel = new TodoListModel();
  constructor(private route: ActivatedRoute, private todoListService: TodoListService) {
  }

  ngOnInit() : void {
    const id = this.route.snapshot.paramMap.get('id');
    this.getList(id)
    console.log(this.todoListModel)
  }

  getList(id : any) : void {
    this.todoListService.getList(id).subscribe(res => {
      this.todoListModel = res;
      console.log(res);
      console.log(this.todoListModel)
    });
  }

  saveChanges(todoListModel: any) : void {
    this.todoListService.updateList(todoListModel).subscribe( res => {
      console.log(res);
    })

  }
}
