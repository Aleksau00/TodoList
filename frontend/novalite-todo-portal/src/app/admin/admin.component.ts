import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {TodoUserService} from "../services/todo-user.service";
import {UserResponseModel} from "../model/UserResponse.model";
import {
  MatCard,
  MatCardActions,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardTitle,
} from "@angular/material/card";
import {CommonModule} from "@angular/common";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    MatCard,
    MatCardHeader,
    MatCardContent,
    MatCardActions,
    MatCardTitle,
    CommonModule,MatCardFooter,
    MatButton
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent {

  users: UserResponseModel[] = [];
  constructor(private userService: TodoUserService, private router: Router) {

  }

  ngOnInit() : void {
    this.getAll();
  }

  getAll() : void {
    this.userService.getAll().subscribe(res => {
      this.users = res;
      console.log(this.users)
    });
  }

  makeManager(user : UserResponseModel) {
    user.role = 1;
    try {
      this.userService.update(user).subscribe(res=> {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate([this.router.url]);
      });

    } catch (error) {
      console.log(error);
    }
  }

  makeUser(user : UserResponseModel) {
    user.role = 0;
    try {
      this.userService.update(user).subscribe(res=> {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate([this.router.url]);
      });

    } catch (error) {
      console.log(error);
    }
  }

}
