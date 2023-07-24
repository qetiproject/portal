import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { ApplicationRef, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { firstValueFrom } from 'rxjs';

import { IndicatorsModule } from '@progress/kendo-angular-indicators';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { UploadsModule } from '@progress/kendo-angular-upload';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { IconsModule } from "@progress/kendo-angular-icons";
import { PopupModule } from '@progress/kendo-angular-popup';
import { GridModule } from '@progress/kendo-angular-grid';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';

import { NotAuthLayoutComponent } from './shared/components/not-auth-layout/not-auth-layout.component';
import { AuthLayoutComponent } from './shared/components/auth-layout/auth-layout.component';
import { LayoutComponent } from './shared/components/layout/layout.component';

import { environment } from 'src/environments/environment.development';

import { BASE_PATH, ResourceService } from './shared/infrastructure/PortalHttpClient';

import { JwtInterceptor } from './modules/authentication-module/interceptor/jwt.interceptor';
import { EventBus } from './shared/infrastructure/CustomApi/event.bus';
import { AuthenticationModuleModule, EmploymentModuleModule, NonComplianceModuleModule, ParametresModuleModule, RoleModuleModule, TimeOffRequestModuleModule } from './modules/module';


let angularModules = [
    CommonModule,
    BrowserModule,  
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
]

let kendoModules = [
    LayoutModule, 
    ButtonsModule,
    GridModule,
    IndicatorsModule,
    IconsModule,
    PopupModule,
    DialogsModule,
    UploadsModule,
]

let appModules = [
    AppRoutingModule,
    EmploymentModuleModule,
    AuthenticationModuleModule,
    TimeOffRequestModuleModule,
    NonComplianceModuleModule,
    ParametresModuleModule,
    RoleModuleModule
]

let customComponents = [
    AppComponent,
    LayoutComponent,
    AuthLayoutComponent,
    NotAuthLayoutComponent
]

@NgModule({
    declarations: [
        customComponents,
    ],
    imports: [
        angularModules,
        kendoModules,
        appModules,
    ],
    providers: [
        {
            provide: BASE_PATH,
            useValue: environment.BASE_PATH
        },
        ResourceService,
        EventBus,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        },
    ],
})
export class AppModule {

    constructor(private resourceService: ResourceService) {}

    ngDoBootstrap(appRef: ApplicationRef) {
        this.loadGlobalResources(appRef);
    }

    async loadGlobalResources(appRef: ApplicationRef): Promise<void> {
        try {
            const response = await firstValueFrom(this.resourceService.apiResourceGetresourcesGet());
            if(response) {
                localStorage.setItem('resourceLayout', JSON.stringify(response));
                appRef.bootstrap(AppComponent);
               
            }
          } catch (error) {
            console.log(error);
          }
    }

 }
