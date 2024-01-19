import { Routes, RouterModule } from '@angular/router';
import {OverviewComponent} from "./overview/overview.component";
import {DetailsComponent} from "./details/details.component";
import {NewComponent} from "./new/new.component";

export const routes: Routes = [
  { path: 'lists', component: OverviewComponent},
  { path: 'lists/new', component: NewComponent},
  { path: 'lists/:id', component: DetailsComponent},

];


