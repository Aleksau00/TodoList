import { Routes, RouterModule } from '@angular/router';
import {OverviewComponent} from "./overview/overview.component";
import {DetailsComponent} from "./details/details.component";
import {NewComponent} from "./new/new.component";
import {HomeComponent} from "./home/home.component";
import {FailedComponent} from "./failed/failed.component";
import {AdminComponent} from "./admin/admin.component";
import {ManagerComponent} from "./manager/manager.component";
import {RoleGuard} from "./role.guard";
import {UnauthorizedComponent} from "./unauthorized/unauthorized.component";

export const routes: Routes = [
  { path: 'lists', component: OverviewComponent, canActivate: [RoleGuard],
    data: { expectedRole: 'User' }},
  { path: 'lists/new', component: NewComponent, canActivate: [RoleGuard],
    data: { expectedRole: 'User' }},
  { path: 'lists/:id', component: DetailsComponent, canActivate: [RoleGuard],
    data: { expectedRole: 'User' }},
  { path: '', component: HomeComponent},
  { path: 'login-failed', component: FailedComponent},
  { path: 'admin', component: AdminComponent, canActivate: [RoleGuard],
    data: { expectedRole: 'Admin' }},
  { path: 'manager', component: ManagerComponent, canActivate: [RoleGuard],
    data: { expectedRole: 'Manager' }},
  { path: 'unauthorized', component: UnauthorizedComponent}

];


