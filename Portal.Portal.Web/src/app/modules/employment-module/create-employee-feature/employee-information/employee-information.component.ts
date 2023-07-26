import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { JobType } from 'src/app/shared/infrastructure/PortalHttpClient';

import { enumToArray } from 'src/app/shared/infrastructure/Utils/utils';
import { EmployeeInformationViewStateModel } from './employeeInformationViewStateModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';


@Component({
  selector: 'app-employee-information',
  templateUrl: './employee-information.component.html',
  styleUrls: ['./employee-information.component.scss']
})
export class EmployeeInformationComponent implements OnInit {
  @Input() personalInformation!: FormGroup;

  viewState: EmployeeInformationViewStateModel = {
    rounded: 'large',
    employmentModuleResourceModel: {},
    phoneNumberMask: '000-00-00-00',
    jobTypes: null,
    errorResourceList: {},
  }
 
  constructor(
    private globalResourceService: GlobalResourceService
  ) {};

  ngOnInit(): void {
    this.loadResource();
  };

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.errorResourceList = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.jobTypes = enumToArray(JobType, this.viewState.employmentModuleResourceModel);
  }
}
