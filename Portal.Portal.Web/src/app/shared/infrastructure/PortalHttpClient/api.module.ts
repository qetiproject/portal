import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { EmployeeService } from './api/employee.service';
import { EmployeeProfileService } from './api/employeeProfile.service';
import { FilesService } from './api/files.service';
import { LogInService } from './api/logIn.service';
import { NonComplianceService } from './api/nonCompliance.service';
import { ParametresService } from './api/parametres.service';
import { ResourceService } from './api/resource.service';
import { RolesService } from './api/roles.service';
import { TimeOffRequestService } from './api/timeOffRequest.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    EmployeeService,
    EmployeeProfileService,
    FilesService,
    LogInService,
    NonComplianceService,
    ParametresService,
    ResourceService,
    RolesService,
    TimeOffRequestService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
