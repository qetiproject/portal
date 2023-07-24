import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { GridLayoutModule, TileLayoutModule, StepperModule, LayoutModule } from '@progress/kendo-angular-layout';
import { ExcelModule, GridModule, PDFModule} from '@progress/kendo-angular-grid';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { IndicatorsModule } from "@progress/kendo-angular-indicators";
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { NavigationModule } from '@progress/kendo-angular-navigation';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { UploadsModule } from '@progress/kendo-angular-upload';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { IconsModule } from '@progress/kendo-angular-icons';

import { 
  EmployeePersonalInformationFeatureComponent, 
  EmployeeSkillsAndLanguagesFeatureComponent, 
  EmployeeEmploymentHistoryFeatureComponent, 
  EmployeePayrollBasicFeatureComponent, 
  EmployeeEducationFeatureComponent, 
  EmployeeOtherInfoFeatureComponent, 
  EmployeeJobInfoFeatureComponent, 
  CreateEmployeeFeatureComponent, 
  EmployeeInformationComponent, 
  EmploymentModuleComponent,
  EmployeeRoleComponent, 
} from './component';

import { EmployeeService, EmployeeProfileService } from 'src/app/shared/infrastructure/PortalHttpClient';

import { EmploymentRoutingModule } from './employment-module-routing';


let angularModules = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
]

let kendoModules = [
  GridModule,
  InputsModule,
  DialogsModule,
  DateInputsModule,
  LabelModule,
  ButtonsModule,
  StepperModule,
  DropDownListModule,
  GridLayoutModule,
  TileLayoutModule,
  PDFModule,
  ExcelModule,
  LayoutModule,
  NavigationModule,
  IconsModule,
  UploadsModule,
  IndicatorsModule,
  DropDownsModule
]

let customComponents = [
  EmploymentModuleComponent,
  EmployeeInformationComponent,
  EmployeeRoleComponent,
  CreateEmployeeFeatureComponent,
  EmployeePersonalInformationFeatureComponent,
  EmployeeJobInfoFeatureComponent,
  EmployeeEducationFeatureComponent,
  EmployeePayrollBasicFeatureComponent,
  EmployeeEmploymentHistoryFeatureComponent,
  EmployeeSkillsAndLanguagesFeatureComponent,
  EmployeeOtherInfoFeatureComponent
]

@NgModule({
  declarations: [
    customComponents,
  ],
  imports: [
    angularModules,
    kendoModules,
    EmploymentRoutingModule
  ],
  exports: [
    EmploymentModuleComponent
  ],
  providers: [
    EmployeeService,
    EmployeeProfileService,
  ],
})
export class EmploymentModuleModule { }
