import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FormFieldModule, TextBoxModule } from '@progress/kendo-angular-inputs';
import { IndicatorsModule } from '@progress/kendo-angular-indicators';
import { ButtonModule } from '@progress/kendo-angular-buttons';
import { LabelModule } from '@progress/kendo-angular-label';

import { LogInService } from 'src/app/shared/infrastructure/PortalHttpClient';

import { AuthenticationRoutingModule } from './authentication-module-routing';

import { ForgotPasswordComponent, LoginFeatureComponent, ResetPasswordComponent, MustMatchDirective } from './component';

let angularModules = [
    CommonModule,
    ReactiveFormsModule
]

let kendoModules = [
    LabelModule,
    FormFieldModule,
    TextBoxModule,
    ButtonModule,
    IndicatorsModule
]

let customComponents = [
    LoginFeatureComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
]

@NgModule({
  declarations: [
    customComponents,
    MustMatchDirective
  ],
  imports: [
    angularModules,
    kendoModules,
    AuthenticationRoutingModule 
  ],
  exports: [
    LoginFeatureComponent,
    ForgotPasswordComponent,
  ],
  providers: [
    LogInService,
  ],
})
export class AuthenticationModuleModule { }
