import { DropDownListModule, DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { NavigationModule } from "@progress/kendo-angular-navigation";
import { LoaderModule } from '@progress/kendo-angular-indicators';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { LabelModule } from '@progress/kendo-angular-label';
import { GridModule } from '@progress/kendo-angular-grid';

import { RoleModuleComponent } from './role-module.component';

import { RoleModuleRoutingModule } from './role-module-routing';

import { RolesService } from 'src/app/shared/infrastructure/PortalHttpClient';

import { RoleDetailComponent } from './role-detail/role-detail.component';


let angularModules = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
]

let kendoModules = [
  GridModule,
  InputsModule,
  LabelModule,
  ButtonsModule,
  DialogModule,
  LoaderModule,
  LayoutModule,
  DropDownListModule, 
  DropDownsModule, 
  NavigationModule
]

let customComponents = [
  RoleModuleComponent
]

@NgModule({
  declarations: [
    customComponents,
    RoleDetailComponent
  ],
  imports: [
    angularModules,
    kendoModules,
    RoleModuleRoutingModule
  ],
  providers: [
    RolesService
  ]
})
export class RoleModuleModule { }
