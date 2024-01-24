import {Component, Inject} from '@angular/core';
import {TodoItemModel} from "../model/todoItem.model";
import {Router} from "@angular/router";
import {TodoListService} from "../services/todoList.service";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MAT_DIALOG_DATA, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {MatButton, MatMiniFabButton} from "@angular/material/button";
import {MatDivider} from "@angular/material/divider";
import {MatButtonToggle, MatButtonToggleGroup} from "@angular/material/button-toggle";

@Component({
  selector: 'app-edit-item',
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
    MatMiniFabButton,
    MatDialogContent,
    MatButtonToggleGroup,
    MatButtonToggle
  ],
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent {

  itemData: any;

  constructor(public dialogRef: MatDialogRef<EditItemComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private router: Router, private service: TodoListService) {
    this.itemData = data;
    this.item = this.itemData.item;
  }

  ngOnInit(): void {
  }
  public item: TodoItemModel = new TodoItemModel();



  public onSave() {
    try {
      this.service.editItem(this.item).subscribe(res => {
        this.dialogRef.close({ status: 'Changes saved successfully' });
      });
    } catch(error) {
      alert(error)
    }
  }

  onCancel() : void {
    this.dialogRef.close();
  }

}
