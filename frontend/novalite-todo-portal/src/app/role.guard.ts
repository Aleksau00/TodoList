// role.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRole = route.data['expectedRole'];

    if (localStorage.getItem('Role') === expectedRole) {
      return true;
    } else {
      // Redirect to unauthorized page or handle as needed
      this.router.navigate(['/unauthorized']);
      return false;
    }
  }
}
