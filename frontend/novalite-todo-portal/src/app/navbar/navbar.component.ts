import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(private router: Router) {
  }

  ngOnInit(): void {

  }

  routeToOverview() : void {
    this.router.navigate(['/lists']);
  }

  routeToNew() : void {
    this.router.navigate(['/lists/new'])
  }

  routeToHome() : void {
    this.router.navigate([''])
  }



}
