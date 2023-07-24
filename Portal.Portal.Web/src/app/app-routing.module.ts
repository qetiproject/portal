import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './modules/authentication-module/guard';
import { GuestGuard } from './modules/authentication-module/guard/guest-guard';
import { LoginFeatureComponent } from './modules/authentication-module/component';
import { ParametreModuleComponent } from './modules/parametres-module/parametres-module.component';

const routes: Routes = [
  {
    path: 'login',
    pathMatch: 'full',
    canActivate: [GuestGuard],
    component: LoginFeatureComponent,
  },
  {
    path: '',
    loadChildren: () => import('./modules/authentication-module/authentication-module.module').then(m => m.AuthenticationModuleModule),
    canActivate: [GuestGuard]
  },
  {
    path: 'management',
    loadChildren: () => import('./modules/employment-module/employment-module.module').then(m => m.EmploymentModuleModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'management/time-off-requests',
    loadChildren: () => import('./modules/time-off-requests-module/time-off-request-module.module').then(m => m.TimeOffRequestModuleModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'management/non-compliance',
    loadChildren: () => import('./modules/non-compliance-module/non-compliance-module.module').then(m => m.NonComplianceModuleModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'management/parametres',
    component: ParametreModuleComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'management/roles',
    loadChildren: () => import('./modules/role-module/role-module.module').then(m => m.RoleModuleModule),
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }