import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { EmployeeRoleViewStateModel } from './employeeRoleViewStateModel';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { EmployeeRolesListModel, EmployeeService, RolesService } from 'src/app/shared/infrastructure/PortalHttpClient';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-employee-role',
  templateUrl: './employee-role.component.html',
  styleUrls: ['./employee-role.component.scss']
})
export class EmployeeRoleComponent implements OnInit{
  @Input('employeeRole') employeeRole!: FormGroup;
  
  viewState: EmployeeRoleViewStateModel = {
    employmentModuleResourceModel: {},
    employeeRoles: [],
    roleId: null,
    roleDetailsResponse: {},
    isLoadingOnGet: false,
    defaultRoleName: '',
    defaultRoleId: null,
  };

  constructor(
    private globalResourceService: GlobalResourceService,
    private employeeHttpClient: EmployeeService,
    private roleHttpClient: RolesService,
  ) {}

  ngOnInit(): void {
    this.loadResource();
    this.employeeRolesView();
  }

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
  }

  async employeeRolesView(): Promise<void> {
    try{
      const response = await firstValueFrom(this.employeeHttpClient.apiEmployeeEmployeeroleslistGet());

      if(response.ok) {
        this.viewState.employeeRoles = response.value.roles;
        this.viewState.defaultRoleName = response.value.roles[0].name;
        this.viewState.defaultRoleId = response.value.roles[0].id;
        this.employeeRole.get('roleId').setValue(this.viewState.defaultRoleId);
        this.roleDetailView(this.viewState.defaultRoleId);
      }
    }catch(error){
      console.log(error);
    }
  }

  roleValueChangeEvent(role: EmployeeRolesListModel): void {
    this.employeeRole.get('roleId').setValue(role.id);
    this.roleDetailView(role.id)
  }

  async roleDetailView(id: number, search?: string): Promise<void> {

    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesRoledetailsGet(id, search));

      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.roleDetailsResponse = response.value;
      } else {
        this.viewState.isLoadingOnGet = false;
      }

    }catch(error){
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

}
