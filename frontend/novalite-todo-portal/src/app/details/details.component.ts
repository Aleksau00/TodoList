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
import {MatIconModule} from "@angular/material/icon";
import {BlobStorageService} from "../services/blob.service";
import {TodoAttachmentService} from "../services/attachment.service";
import {AttachmentsDTOModel} from "./DTOs/AttachmentsDTO.model";
import {AttachmentListComponent} from "../attachment-list/attachment-list.component";

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
    MatMiniFabButton,
    MatIconModule
  ],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {


  todoListModel : TodoListAndItemsDTOModel = new TodoListAndItemsDTOModel();
  newContent: string = "";
  attachments: AttachmentsDTOModel[] = [];
  selectedFile: File | null = null;
  constructor(private attachmentService: TodoAttachmentService, private blobService: BlobStorageService, private dialog: MatDialog,private route: ActivatedRoute, private todoListService: TodoListService, private router: Router) {
  }

  ngOnInit() : void {
    const id = this.route.snapshot.paramMap.get('id');
    this.getList(id);
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
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate([this.router.url]);
      });
    } catch(error) {
      alert(error)
    }
  }

  listAttachments() : void {
    this.attachmentService.getAttachments(this.route.snapshot.paramMap.get('id')).subscribe(
      (attachments) => {
        this.attachments = attachments;
        const dialogConfig = new MatDialogConfig();
        dialogConfig.data =  {attachments : attachments};
        dialogConfig.height = '500px';
        dialogConfig.width = '600px';
        const dialogRef = this.dialog.open(AttachmentListComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(result => {
          console.log(`Dialog result: ${result}`);
        });

        console.log('Attachments:', this.attachments);
      },
      (error) => {
        console.error('Error fetching attachments:', error);
      }
    );
  }

  createReminder() : void {
    try {
      this.todoListService.createReminder(this.todoListModel.todoList.id).subscribe(( res=> {
        alert("Successfully created reminder")
      }))
    } catch (error) {
      alert("Already set a reminder today");
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

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  uploadFile(): void {
    try {
      this.blobService.getSas(this.selectedFile!.name, this.route.snapshot.paramMap.get('id')!).subscribe((response) => {
        const sas = response.sas;
        const blobId = response.attachmentId;

        // Check if this.selectedFile is not null before attempting to upload
        if (this.selectedFile) {
          this.blobService.uploadFile(sas, this.selectedFile, blobId);
        } else {
          console.error('Selected file is null or undefined');
        }
      });
    } catch (error) {
      alert('Upload failed (Check if file is selected)');
    }

  }
}
