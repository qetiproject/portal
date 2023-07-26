import { PageChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectEvent } from '@progress/kendo-angular-layout';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { RoleDetailViewStateModel } from './roleDetailViewStateModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { AddEmployeeRequest, DeleteEmployeeRequest, EditRoleRequest, ResourceResponse, RolesService } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-role-detail',
  templateUrl: './role-detail.component.html',
  styleUrls: ['./role-detail.component.scss']
})
export class RoleDetailComponent implements OnInit{
  layoutResource: ResourceResponse;

  viewState: RoleDetailViewStateModel = {
    tabSelectEventTitle: '',
    activatedParam: null,
    roleDetailsResponse: {},
    isLoadingOnGet: false,
    isEditRolePermissionView: false,
    isLoadingPermissions: false,
    isLoadingOnPost: false,
    breadItems: [],
    selectedItemIndex: null,
    rolesUrl: [],
    rounded: 'large',
    roundedSearch: 'full',
    buttonSize: 'large',
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
    searchEmployeeValue: '',
    isEmployeesView: false,
    isActiveAddEmployeeViewState: false,
    employeeDataView: [],
    activePermissionsTab: true,
    activeEmployeesTab: false,
    isActiveDeleteEmployeeViewState: false,
    employeeId: null,
    permissionsResponse: {},
    employeeFullName: '',
  }

  editRoleForm = new FormGroup({
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

  addEmployeeForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  })

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private roleHttpClient: RolesService,
    private globalResourceService: GlobalResourceService,
  ) {
    this.viewState.activatedParam = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.loadResource();
    this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
  }

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
    this.viewState.breadItems = [
      {
        text: this.layoutResource.rolesResources.rolesList,
        url: 'management/roles',
      },
      {
        text: 'Role Detail',
      },
    ];
  }

  breadItemsViewState(item: any): void {
    this.viewState.selectedItemIndex = this.viewState.breadItems.findIndex((i) => i.text === item.text);
    this.viewState.rolesUrl = this.viewState.breadItems
      .slice(0, this.viewState.selectedItemIndex + 1)
      .map((i) => i.url);
    this.router.navigate(this.viewState.rolesUrl);
  };

  rowStyling = (context: RowClassArgs) => {
    if (context.index %2 !=0) {
      return { grey: true };
    } else {
      return { white: true };
    }
  }
  
  tabSelectButtonClickEvent(event: SelectEvent) : void {
    localStorage.setItem('tabSelectEventTitle', event.title);
    let tabSelectEventTitle = localStorage.getItem('tabSelectEventTitle');

    switch(tabSelectEventTitle) {
      case this.layoutResource.rolesResources.permissions:
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
        this.viewState.activeEmployeesTab = false;
        this.viewState.activePermissionsTab = true;
        this.viewState.isEmployeesView = false;
        break;
      case this.layoutResource.rolesResources.employees:
        this.viewState.activeEmployeesTab = true;
        this.viewState.activePermissionsTab = false;
        this.viewState.isEmployeesView = true;
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
        break;
      default:
        localStorage.setItem('tabSelectEventTitle', this.layoutResource.rolesResources.permissions);
        this.viewState.activePermissionsTab = true;
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
    }
  }

  async roleDetailView(id: number, search: string): Promise<void> {

    try{
      // this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesRoledetailsGet(id, search));

      if(response.ok) {
        // this.viewState.isLoadingOnGet = false;
        this.viewState.roleDetailsResponse = response.value;
      } else {
        // this.viewState.isLoadingOnGet = false;
        this.viewState.isEditRolePermissionView = false;
      }

    }catch(error){
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async permissionsView(roleId: number): Promise<void> {

    try{
      this.viewState.isLoadingPermissions = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesPermissionsGet(roleId));

      if(response.ok) {
        this.viewState.isLoadingPermissions = false;
        this.viewState.permissionsResponse = response.value;
        this.editRoleViewForm(this.viewState.permissionsResponse);

      }else {
        this.viewState.isLoadingPermissions = false;
      }

    }catch(error){
      this.viewState.isLoadingPermissions = false;
      console.log(error);
    }
  }
  

  rolePermissionEditButtonClickView(): void {
    this.viewState.isEditRolePermissionView = true;
    this.permissionsView(this.viewState.activatedParam);
    this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
  }

  editRoleViewForm(permissionValue): void {
    this.editRoleForm.get('roleName').setValue(this.viewState.roleDetailsResponse.name),
    this.editRoleForm.get('description').setValue(this.viewState.roleDetailsResponse.description),
    this.editRoleForm.get('addEmployeePermission').setValue(permissionValue.employeeModule.addEmployeePermission.active);
    this.editRoleForm.get('viewEmployeesListPermission').setValue(permissionValue.employeeModule.viewEmployeesListPermission.active);
    this.editRoleForm.get('editPersonalInformationPermission').setValue(permissionValue.employeeModule.editPersonalInformationPermission.active);
    this.editRoleForm.get('editEmploymentHistoryPermission').setValue(permissionValue.employeeModule.editEmploymentHistoryPermission.active);
    this.editRoleForm.get('editJobInfoPermission').setValue(permissionValue.employeeModule.editJobInfoPermission.active);
    this.editRoleForm.get('editEducationPermission').setValue(permissionValue.employeeModule.editEducationPermission.active);
    this.editRoleForm.get('editPayrollBasicPermission').setValue(permissionValue.employeeModule.editPayrollBasicPermission.active);
    this.editRoleForm.get('editSkillsAndLanguagesPermission').setValue(permissionValue.employeeModule.editSkillsAndLanguagesPermission.active);
    this.editRoleForm.get('editOtherInfoPermission').setValue(permissionValue.employeeModule.editOtherInfoPermission.active);

    this.editRoleForm.get('viewAdminPanelPermission').setValue(permissionValue.adminPanelModule.viewAdminPanelPermission.active);
    this.editRoleForm.get('allTimeOffRequestsPermission').setValue(permissionValue.timeOffRequestModule.allTimeOffRequestsPermission.active);
    this.editRoleForm.get('allNonComliancesPermission').setValue(permissionValue.nonComlianceModule.allNonComliancesPermission.active);
    this.editRoleForm.get('viewRolesPermission').setValue(permissionValue.rolesModel.viewRolesPermission.active);
  }

  async updateRoleButtonClickEvent(editRoleFormValue): Promise<void> {

    let editRoleRequest: EditRoleRequest = {
      roleId: this.viewState.activatedParam,
      addEmployeePermission: editRoleFormValue.addEmployeePermission,
      viewEmployeesListPermission: editRoleFormValue.viewEmployeesListPermission,
      editPersonalInformationPermission: editRoleFormValue.editPersonalInformationPermission,
      editJobInfoPermission: editRoleFormValue.editJobInfoPermission,
      editEducationPermission: editRoleFormValue.editEducationPermission,
      editPayrollBasicPermission: editRoleFormValue.editPayrollBasicPermission,
      editEmploymentHistoryPermission: editRoleFormValue.editEmploymentHistoryPermission,
      editSkillsAndLanguagesPermission: editRoleFormValue.editSkillsAndLanguagesPermission,
      editOtherInfoPermission: editRoleFormValue.editOtherInfoPermission,
      allTimeOffRequestsPermission: editRoleFormValue.allTimeOffRequestsPermission,
      allNonComliancesPermission: editRoleFormValue.allNonComliancesPermission,
      viewAdminPanelPermission: editRoleFormValue.viewAdminPanelPermission,
      viewRolesPermission: editRoleFormValue.viewRolesPermission,
      name: editRoleFormValue.roleName, 
      description: editRoleFormValue.description,
    }

    try{
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesEditrolePost(editRoleRequest));
      if(response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isEditRolePermissionView = false;
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
      }else {
        this.viewState.isLoadingOnPost = true;
      }

    }catch(error) {
      console.log(error);
    }
  }

  cancelUpdateRoleClickView(): void {
    this.viewState.isEditRolePermissionView = false;
  }

  employeeFilterInputClickEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.roleDetailView(this.viewState.activatedParam, inputValue);
  }
  
  employeePageChangeClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
    this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
  }

  async addEmployeeButtonClickView(): Promise<void> {
    this.viewState.isActiveAddEmployeeViewState = true;
    this.employeeDataView('');
  }

  async employeeDataView(data: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.roleHttpClient.apiRolesEmployeesGet(data));

      if(response.ok) {
        this.viewState.employeeDataView = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  }

  employeeListFilterEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.employeeDataView(inputValue);
  }

  async addEmployeeButtonClickEvent(addEmployeeFormValue): Promise<void> {

    if (!this.addEmployeeForm.valid) {
      this.addEmployeeForm.markAllAsTouched();
      return;
    } 

    let addEmployeeRequest: AddEmployeeRequest = {
      roleId: this.viewState.activatedParam,
      email: addEmployeeFormValue.email
    }

    try{
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesAddemployeePost(addEmployeeRequest));

      if(response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isActiveAddEmployeeViewState = false;
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
        this.addEmployeeForm.reset();
      }else {
        this.viewState.isLoadingOnPost = false;
      }

    }catch(error){
      console.log(error);
    }

  }

  async deleteEmployeeButtonClickEvent(): Promise<void> {

    let deleteEmployeeRequest: DeleteEmployeeRequest = {
      roleId: Number(this.viewState.activatedParam),
      userId: this.viewState.employeeId,
    }

    try{
      const response = await firstValueFrom(this.roleHttpClient.apiRolesDeleteemployeePost(deleteEmployeeRequest));
      if(response.ok) {
        this.roleDetailView(this.viewState.activatedParam, this.viewState.searchEmployeeValue);
        this.viewState.isActiveDeleteEmployeeViewState = false;
      }

    }catch(error){
      console.log(error);
    }
  }

  cancelAddEmployeeClickView(): void {
    this.viewState.isActiveAddEmployeeViewState = false;
    this.addEmployeeForm.reset();
  }

  closeAddEmployeeFormClickView(): void {
    this.viewState.isActiveAddEmployeeViewState = false;
    this.addEmployeeForm.reset();
  }
  
  deleteEmployeeDialogView(employee): void {
    this.viewState.isActiveDeleteEmployeeViewState = true;
    this.viewState.employeeId = employee.userId;
    this.viewState.employeeFullName = employee.fullName;
  }

  closeDeleteEmployeeDialogView(): void {
    this.viewState.isActiveDeleteEmployeeViewState = false;
  }

  cancelDeleteEmployeeDialogView(): void {
    this.viewState.isActiveDeleteEmployeeViewState = false;
  }

}
