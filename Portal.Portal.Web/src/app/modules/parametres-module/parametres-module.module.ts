import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { TileLayoutModule, LayoutModule } from '@progress/kendo-angular-layout';

import { ParametreModuleComponent } from './parametres-module.component';
import { ParametresService } from 'src/app/shared/infrastructure/PortalHttpClient';

let angularModules = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
]

let kendoModules = [
  InputsModule,
  LabelModule,
  ButtonsModule,
  TileLayoutModule,
  LayoutModule,
  DropDownsModule
]

let customComponents = [
  ParametreModuleComponent
]

@NgModule({
  declarations: [
    customComponents,
  ],
  imports: [
    angularModules,
    kendoModules,
  ],
  exports: [],
  providers: [
    ParametresService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ParametresModuleModule { }
