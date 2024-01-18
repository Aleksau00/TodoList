import { Routes, RouterModule } from '@angular/router';
import {OverviewComponent} from "./overview/overview.component";
import {DetailsComponent} from "./details/details.component";
import {NewComponent} from "./new/new.component";

export const routes: Routes = [
  { path: '', component: OverviewComponent},
  { path: 'details', component: DetailsComponent},
  { path: 'new', component: NewComponent}
];


