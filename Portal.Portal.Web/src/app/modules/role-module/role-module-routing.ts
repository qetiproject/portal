import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { RoleDetailComponent, RoleModuleComponent } from './component';


const routes: Routes = [
  {
    path: '',
    component: RoleModuleComponent
  },
  {
    path: 'role-detail/:id',
    component: RoleDetailComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleModuleRoutingModule { }
