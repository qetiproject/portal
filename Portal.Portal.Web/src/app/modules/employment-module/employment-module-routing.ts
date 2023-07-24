import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EmployeeEducationFeatureComponent, EmployeeEmploymentHistoryFeatureComponent, EmployeeJobInfoFeatureComponent, EmployeeOtherInfoFeatureComponent, EmployeePayrollBasicFeatureComponent, EmployeePersonalInformationFeatureComponent, EmployeeSkillsAndLanguagesFeatureComponent, EmploymentModuleComponent } from './component';


const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: 'employee-service',
    },
    {
        path: 'employee-service',
        component: EmploymentModuleComponent
    },
    { 
        path: 'employee-service/employee-profile/:id', 
        component: EmployeePersonalInformationFeatureComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            redirectTo: 'job-info',
          },
          {
            path: 'job-info',
            component: EmployeeJobInfoFeatureComponent
          },
          {
            path: 'education',
            component: EmployeeEducationFeatureComponent
          },
          {
            path: 'payroll-basic',
            component: EmployeePayrollBasicFeatureComponent
          },
          {
            path: 'employment-history',
            component: EmployeeEmploymentHistoryFeatureComponent
          },
          {
            path: 'skills-and-languages',
            component: EmployeeSkillsAndLanguagesFeatureComponent
          },
          {
            path: 'other-info',
            component: EmployeeOtherInfoFeatureComponent
          },
        ]
    },
    
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmploymentRoutingModule { }