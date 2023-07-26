import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { RoleViewStateModel } from './roleViewStateModel';
import { PageChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { CreateRoleRequest, ResourceResponse, RolesListModel, RolesService } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-role-module',
  templateUrl: './role-module.component.html',
  styleUrls: ['./role-module.component.scss']
})
export class RoleModuleComponent implements OnInit{
 
  layoutResource: ResourceResponse;

  viewState: RoleViewStateModel = {
    errorResourceModel: {},
    rounded: 'large',
    roundedSearch: 'full',
    buttonSize: 'large',
    isLoadingOnGet: false,
    initialRoleListViewModel: [],
    roleListViewModel: [],
    gridList: {
      variablesForPageSizes: {
        buttonCount: 5,
        pageSizes: [5, 10, 20, 50],
        pageSize: 10,
        skip: 0,
      },
      sortable: true,
      filter: true,
    },
    searchRoleValue: '',
    isActiveCreateRoleViewState: false,
    isLoadingOnPost: false,
    permissionsResponse: {},
    isLoadingPermissions: false,
  }
  
  createRoleForm = new FormGroup({
    roleName: new FormControl('', Validators.required),
    addEmployeePermission: new FormControl(),
    viewEmployeesListPermission: new FormControl(),
    allTimeOffRequestsPermission: new FormControl(),
    allNonComliancesPermission: new FormControl(),
    roleBased: new FormControl(),
    description: new FormControl('', Validators.required),
    editPersonalInformationPermission: new FormControl(),
    editEmploymentHistoryPermission: new FormControl(),
    editJobInfoPermission: new FormControl(),
    editEducationPermission: new FormControl(),
    editPayrollBasicPermission: new FormControl(),
    editSkillsAndLanguagesPermission: new FormControl(),
    editOtherInfoPermission: new FormControl(),
    viewAdminPanelPermission: new FormControl(),
    viewRolesPermission: new FormControl(),
  })

  constructor(
    private globalResourceService: GlobalResourceService,
    private roleHttpClient: RolesService
  ) {}

  ngOnInit(): void {
    this.loadResource();
    this.roleListView(this.viewState.searchRoleValue);
    this.permissionsView();
  }

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
    this.viewState.errorResourceModel = this.globalResourceService.resourceResponse;
  }

  rowStyling = (context: RowClassArgs) => {
    if (context.index %2 !=0) {
      return { grey: true };
    } else {
      return { white: true };
    }
  }

  createRoleButtonClickView(): void {
    this.viewState.isActiveCreateRoleViewState = true;
    this.permissionsView();
  }

  async roleListView(search: string): Promise<void> {

    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesRoleslistGet(search));
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.initialRoleListViewModel = response.value.roles;
        this.viewState.roleListViewModel =  response.value.roles;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }

  }

  async permissionsView(roleId?: number): Promise<void> {

    try{
      this.viewState.isLoadingPermissions = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesPermissionsGet(roleId));

      if(response.ok) {
        this.viewState.isLoadingPermissions = false;
        this.viewState.permissionsResponse = response.value;
        
        this.createRoleForm.get('addEmployeePermission').setValue(this.viewState.permissionsResponse.employeeModule.addEmployeePermission.active);
        this.createRoleForm.get('viewEmployeesListPermission').setValue(this.viewState.permissionsResponse.employeeModule.addEmployeePermission.active);
        this.createRoleForm.get('editPersonalInformationPermission').setValue(this.viewState.permissionsResponse.employeeModule.editPersonalInformationPermission.active);
        this.createRoleForm.get('editEmploymentHistoryPermission').setValue(this.viewState.permissionsResponse.employeeModule.editEmploymentHistoryPermission.active);
        this.createRoleForm.get('editJobInfoPermission').setValue(this.viewState.permissionsResponse.employeeModule.editJobInfoPermission.active);
        this.createRoleForm.get('editEducationPermission').setValue(this.viewState.permissionsResponse.employeeModule.editEducationPermission.active);
        this.createRoleForm.get('editPayrollBasicPermission').setValue(this.viewState.permissionsResponse.employeeModule.editPayrollBasicPermission.active);

        this.createRoleForm.get('editSkillsAndLanguagesPermission').setValue(this.viewState.permissionsResponse.employeeModule.editSkillsAndLanguagesPermission.active);
        this.createRoleForm.get('editOtherInfoPermission').setValue(this.viewState.permissionsResponse.employeeModule.editOtherInfoPermission.active);
        this.createRoleForm.get('viewAdminPanelPermission').setValue(this.viewState.permissionsResponse.adminPanelModule.viewAdminPanelPermission.active);

        this.createRoleForm.get('allTimeOffRequestsPermission').setValue(this.viewState.permissionsResponse.timeOffRequestModule.allTimeOffRequestsPermission.active);
        this.createRoleForm.get('allNonComliancesPermission').setValue(this.viewState.permissionsResponse.nonComlianceModule.allNonComliancesPermission.active);
        this.createRoleForm.get('viewRolesPermission').setValue(this.viewState.permissionsResponse.rolesModel.viewRolesPermission.active);
       

      }else {
        this.viewState.isLoadingPermissions = false;
      }

    }catch(error){
      this.viewState.isLoadingPermissions = false;
      console.log(error);
    }
  }

  rolePageChangeClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  roleFilterInputClickEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value.toLowerCase();
    this.viewState.roleListViewModel = this.viewState.initialRoleListViewModel.filter((element): any => {
      if(element.name.toLowerCase().includes(inputValue) || element.description.toLowerCase().includes(inputValue) || element.numberOfEmployees.toString().includes(inputValue)) {
        return element;
      }
    });
  }

  roleChangeEvent(role: RolesListModel): void {
    this.permissionsView(role.id)
  }

  async createRoleButtonClickEvent(createRoleFormValue): Promise<void> {

    if (!this.createRoleForm.valid) {
      this.createRoleForm.markAllAsTouched();
      return;
    } 
    
    let createRoleRequest: CreateRoleRequest = {
      addEmployeePermission: createRoleFormValue.addEmployeePermission,
      viewEmployeesListPermission: createRoleFormValue.viewEmployeesListPermission,
      editPersonalInformationPermission: createRoleFormValue.editPersonalInformationPermission,
      editJobInfoPermission: createRoleFormValue.editJobInfoPermission,
      editEducationPermission: createRoleFormValue.editEducationPermission,
      editPayrollBasicPermission: createRoleFormValue.editPayrollBasicPermission,
      editEmploymentHistoryPermission: createRoleFormValue.editEmploymentHistoryPermission,
      editSkillsAndLanguagesPermission: createRoleFormValue.editSkillsAndLanguagesPermission,
      editOtherInfoPermission: createRoleFormValue.editOtherInfoPermission,
      allTimeOffRequestsPermission: createRoleFormValue.allTimeOffRequestsPermission,
      allNonComliancesPermission: createRoleFormValue.allNonComliancesPermission,
      viewAdminPanelPermission: createRoleFormValue.viewAdminPanelPermission,
      viewRolesPermission: createRoleFormValue.viewRolesPermission,
      name: createRoleFormValue.roleName, 
      description: createRoleFormValue.description,
    }

    try{
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesCreaterolePost(createRoleRequest));

      if(response.ok) {
        this.viewState.isActiveCreateRoleViewState = false;
        this.createRoleForm.reset();
        this.viewState.isLoadingOnPost = false;
        this.roleListView(this.viewState.searchRoleValue);
      }else {
        this.viewState.isLoadingOnPost = false;
      }

    }catch(error){
      console.log(error);
      this.viewState.isLoadingOnPost = false;
    }
    
  }

  closeCreateRoleFormClickEvent(): void {
    this.viewState.isActiveCreateRoleViewState = false;
  }

  cancelCreateRoleClickEvent(): void {
    this.viewState.isActiveCreateRoleViewState = false;
  }

}
