import { 
  FormsModule, 
  ReactiveFormsModule, 
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { 
  GridLayoutModule, 
  TileLayoutModule,
  LayoutModule, 
} from '@progress/kendo-angular-layout';
import { 
  DropDownListModule, 
  DropDownsModule, 
} from '@progress/kendo-angular-dropdowns';
import { IndicatorsModule } from "@progress/kendo-angular-indicators";
import { NavigationModule } from "@progress/kendo-angular-navigation";
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { UploadsModule } from "@progress/kendo-angular-upload";
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { GridModule } from '@progress/kendo-angular-grid';
import { TooltipsModule } from '@progress/kendo-angular-tooltip';

import { TimeOffRequestService } from 'src/app/shared/infrastructure/PortalHttpClient';

import { TimeOffRequestsModuleComponent, TimeOffRequestRecievedHistoryComponent, TimeOffRequestSentHistoryComponent, TimeOffRequestAllHistoryComponent } from './component';

import { TimeOffRequestsRoutingModule } from './time-off-requests-module-routing';

let angularModules = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
]

let kendoModules = [
  GridModule,
  InputsModule,
  DateInputsModule,
  LabelModule,
  ButtonsModule,
  DropDownListModule,
  GridLayoutModule,
  TileLayoutModule,
  LayoutModule,
  DialogModule,
  UploadsModule,
  DropDownsModule,
  NavigationModule,
  IndicatorsModule,
  TooltipsModule
]

let customComponents = [
  TimeOffRequestsModuleComponent,
  TimeOffRequestRecievedHistoryComponent,
  TimeOffRequestSentHistoryComponent,
  TimeOffRequestAllHistoryComponent,
]

@NgModule({
  declarations: [
    customComponents,
  ],
  imports: [
    angularModules,
    kendoModules,
    TimeOffRequestsRoutingModule
  ],
  exports: [
  ],
  providers: [
    TimeOffRequestService,
    DatePipe
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class TimeOffRequestModuleModule { }
