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
import {MatButton, MatMiniFabButton} from "@angular/material/button";
import {TodoListAndItemsDTOModel} from "../model/todoListAndItemsDTO.model";
import {MatDivider} from "@angular/material/divider";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {EditItemComponent} from "../edit-item/edit-item.component";

@Component({
  selector: 'app-details',
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
    MatButton,
    MatDivider,
    MatMiniFabButton
  ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {


  todoListModel : TodoListAndItemsDTOModel = new TodoListAndItemsDTOModel();
  newContent: string = "";
  constructor(private dialog: MatDialog,private route: ActivatedRoute, private todoListService: TodoListService, private router: Router) {
  }

  ngOnInit() : void {
    const id = this.route.snapshot.paramMap.get('id');
    this.getList(id)
  }

  getList(id : any) : void {
    this.todoListService.getList(id).subscribe(res => {
      this.todoListModel = res;
      console.log(res)
    });
  }

  saveChanges(todoListModel: any) : void {
    this.todoListService.updateList(todoListModel).subscribe( res => {
    })

  }

  createItem() : void {
    try {
      this.todoListService.createItem(this.newContent, this.todoListModel.todoList.id).subscribe(res => {
        alert("Success")
      });
    } catch(error) {
      alert(error)
    }
  }

  editItem(item: any, listId: string) : void{
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data =  {item : item, listId : listId};
    dialogConfig.height = '300px';
    const dialogRef = this.dialog.open(EditItemComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
    });
  }

  goToOverview() {
    this.router.navigate(['/lists'])
  }
}
