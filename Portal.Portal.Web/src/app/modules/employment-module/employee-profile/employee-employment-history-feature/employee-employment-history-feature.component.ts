import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription, firstValueFrom } from 'rxjs';

import { employeeEmploymentHistoryVIewModel } from './employeeEmploymentHistoryVIewModel';

import { AddEmployeeToRoleRequest, BASE_PATH, DeleteEmployeeRequest, EmployeeProfileService, EmployeeRolesListModel, EmployeeService, FileType, FormerPositionModel, RolesService } from 'src/app/shared/infrastructure/PortalHttpClient';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-employee-employment-history-feature',
  templateUrl: './employee-employment-history-feature.component.html',
  styleUrls: ['./employee-employment-history-feature.component.scss']
})

export class EmployeeEmploymentHistoryFeatureComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];

  viewState: employeeEmploymentHistoryVIewModel = {
    activatedParam: null,
    basePath: '',
    employmentModuleResourceModel: {},
    employmentErrorResourceModel: {},
    employeeEmploymentHistoryViewModel: {},
    isEmployeeEmploymentHistoryView: true,
    editEmployeeEmploymentHistoryViewModel: {},
    contractSelectedFile: null,
    contractFileExtensionOnUpload: '',
    isFormerPositionFormViewStateOpened: false,
    isFormerPositionFormEditViewState: false,
    isFormerPositionDeleteModalOpened: false,
    formerPositionToDelete: null,
    addEmployeeFormerPositionView: {},
    editEmployeeFormerPositionView: {},
    deleteEmployeeFormerPositionView: {},
    monthsResourceModel: null,
    fileTypes: FileType,
    isLoadingOnGet: false,
    isLoadingOnPost: false,
    selectedFilesChanged: false,
    supervisorDataView: [],
    initialSupervisor: [],
    contractTypes: [],
    employeeRoles: [],
    employeeRoleName: '',
    employeeRoleId: null,
    actionPermissions: {},
    isEmployeeRoleView: true,
    isLoadingOnPostRole: false,
  }

  employeeEmploymentHistoryForm = new FormGroup({
    contractType: new FormControl(''),
    jobStartDate: new FormControl(null, [this.employeeEmploymentHistoryFormFormForbiddenDates.bind(this)]),
    contractExpirationDate: new FormControl(null, [this.employeeEmploymentHistoryFormFormForbiddenDates.bind(this)]),
    supervisor: new FormControl(''),
  });

  employeeFormerPositionForm = new FormGroup({
    formerPositionId: new FormControl(null),
    title: new FormControl('', [Validators.required]),
    startDate: new FormControl(null, [Validators.required, this.formerPositionsFormForbiddenDates.bind(this)]),
    endDate: new FormControl(null, [Validators.required, this.formerPositionsFormForbiddenDates.bind(this)])
  });

  employeeRoleForm = new FormGroup({
    employeeRole: new FormControl(null),
  })

  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private employeeProfileService: EmployeeProfileService,
    private employeeHttpClient: EmployeeService,
    private roleHttpClient: RolesService,
    @Inject(BASE_PATH) basePath: string,
  ) {
    this.viewState.activatedParam = +this.router.url.split('/')[4];
    this.viewState.basePath = `${basePath}`;
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  };

  ngOnInit(): void {
    this.employementModuleResource();
    this.employeeEmploymentHistoryView(this.viewState.activatedParam);
    this.employeeEmploymentHistoryFormDateControlsSubscribe();
    this.employeeFormerPositionFormDateControlsSubscribe();
  };

  employementModuleResource(): void {
      this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
      this.viewState.employmentErrorResourceModel = this.globalResourceService.resourceResponse.errorResources;
      this.viewState.monthsResourceModel = Object.values(this.globalResourceService.resourceResponse.monthResources);
  };

  async employeesContractTypeData(): Promise<void> {
    try {
    const response  = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileContracttypesGet());
    if(response.ok) {
      this.viewState.contractTypes = response.value.contractTypes;
    }
    } catch(error) {
      console.log(error);
    }
  }

  async supervisorDataView(id: number, search: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileSupervisoremployeesGet(id, search));

      if(response.ok) {
        this.viewState.initialSupervisor = response.value.employees;
        this.viewState.supervisorDataView = response.value.employees;
      }
    }catch(error){
      console.log(error);
    }
  };

  async employeeRolesView(): Promise<void> {
    try{
      const response = await firstValueFrom(this.employeeHttpClient.apiEmployeeEmployeeroleslistGet());

      if(response.ok) {
        this.viewState.employeeRoles = response.value.roles;
      }
    }catch(error){
      console.log(error);
    }
  }

  employeeRoleEditButtonView(): void {
    this.viewState.isEmployeeRoleView = false;
  }

  roleValueChangeEvent(role: EmployeeRolesListModel): void {
    this.viewState.employeeRoleId = role.id;
    this.viewState.employeeRoleName = role.name;
  }
  
  supervisorFilterEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.supervisorDataView(this.viewState.activatedParam, inputValue);
  }

  async employeeEmploymentHistoryView(id: number): Promise<void> {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmploymenthistoryGet(id));
        if(response.ok) {
          this.viewState.isLoadingOnGet = false;
          this.viewState.employeeEmploymentHistoryViewModel = response.value;
          this.viewState.employeeEmploymentHistoryViewModel.formerPositions = this.viewState.employeeEmploymentHistoryViewModel.formerPositions.map((formerPosition) => {
            formerPosition.startDate = new Date(formerPosition.startDate);
            formerPosition.endDate = new Date(formerPosition.endDate.toString().split('T')[0]);
            return formerPosition;
          });
          this.employeeRolesView();
        } else {
          this.viewState.isLoadingOnGet = false;
        }    
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error, 'error');
    }
  };

  employeeEmploymentHistoryEditButtonClickEvent(): void {
    this.employeesContractTypeData();
    this.supervisorDataView(this.viewState.activatedParam, '');
    this.viewState.isEmployeeEmploymentHistoryView = false;
    this.employeeEmploymentHistoryForm.setValue({
      contractType: this.viewState.employeeEmploymentHistoryViewModel.contractType,
      jobStartDate: this.viewState.employeeEmploymentHistoryViewModel.jobStartDate ? new Date(this.viewState.employeeEmploymentHistoryViewModel.jobStartDate) : null,
      contractExpirationDate: this.viewState.employeeEmploymentHistoryViewModel.contractExpirationDate ? new Date(this.viewState.employeeEmploymentHistoryViewModel.contractExpirationDate) : null,
      supervisor: this.viewState.employeeEmploymentHistoryViewModel.supervisor,
    });
  };

  employeeEmploymentHistoryContractFileSelectedButtonCliekEvent(contract): void {
    this.viewState.contractSelectedFile = contract.target.files[0];
    if(contract.target.files[0].name) {
      this.viewState.contractFileExtensionOnUpload = assignFileTypeForUpload(contract);
    } else {
      this.viewState.contractFileExtensionOnUpload = '';
    }
    contract.target.value = '';
  }

  employeeEmploymentHistoryContractFileDeleteButtonCliekEvent(): void {
    this.viewState.contractSelectedFile = null;
    this.viewState.employeeEmploymentHistoryViewModel.contract = null;
    this.viewState.selectedFilesChanged = true;
  }

  async saveEmployeeEmploymentHistoryButtonClickEvent(): Promise<void> {
    this.viewState.editEmployeeEmploymentHistoryViewModel = {
      employeeId: this.viewState.activatedParam,
      contractType: this.employeeEmploymentHistoryForm.value.contractType,
      jobStartDate: this.employeeEmploymentHistoryForm.value.jobStartDate,
      contractExpirationDate: this.employeeEmploymentHistoryForm.value.contractExpirationDate,
      supervisor: this.employeeEmploymentHistoryForm.value.supervisor,
    }

    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemploymenthistoryPost(this.viewState.editEmployeeEmploymentHistoryViewModel));

      let saveEmployeeEmploymentHistoryContractFileEvent;
      if(this.viewState.contractSelectedFile || (!this.viewState.contractSelectedFile && !this.viewState.employeeEmploymentHistoryViewModel.contract?.name)) {
        saveEmployeeEmploymentHistoryContractFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadcontractPostForm(
          this.viewState.activatedParam,
          this.viewState.contractSelectedFile,
          ));
        }

      Promise.all([response, saveEmployeeEmploymentHistoryContractFileEvent]).then((result) => {
        if(response.ok && !saveEmployeeEmploymentHistoryContractFileEvent?.errors) {
          this.viewState.isEmployeeEmploymentHistoryView = true;
          this.viewState.contractSelectedFile = null;
          this.employeeEmploymentHistoryView(this.viewState.activatedParam);
        } else {
          this.viewState.isLoadingOnGet = false;
        }
      });

    } catch (error) {
      this.viewState.isLoadingOnGet = false;
      console.error(error);
    }
  };

  async saveEmployeeRoleButtonClickEvent(employeeRoleFormValue): Promise<void> {

    let addEmployeeToRoleRequest: AddEmployeeToRoleRequest = {
      roleId: employeeRoleFormValue.employeeRole.id,
      userId: this.viewState.activatedParam,
    }

    try{
      this.viewState.isLoadingOnPostRole = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddemployeetorolePost(addEmployeeToRoleRequest));

      if(response.ok) {
          this.viewState.isLoadingOnPostRole = false;
          this.viewState.isEmployeeRoleView = true;
          this.employeeEmploymentHistoryView(this.viewState.activatedParam);
      }

    }catch(error) {
      this.viewState.isLoadingOnPostRole = false;
      console.log(error)
    }
  }

  cancelEmployeeRoleButtonClickEvent(): void {
    this.viewState.isEmployeeRoleView = true;
  }

  async deleteEmployeeRoleButtonClickEvent(role): Promise<void> {

    let deleteEmployeeRequest: DeleteEmployeeRequest = {
      roleId: role.roleId,
      userId: Number(this.viewState.activatedParam),
    }

    try{
      this.viewState.isLoadingOnPostRole = true;
      const response = await firstValueFrom(this.roleHttpClient.apiRolesDeleteemployeePost(deleteEmployeeRequest));
      if(response.ok) {
        this.viewState.isLoadingOnPostRole = false;
        this.employeeEmploymentHistoryView(this.viewState.activatedParam);
        this.viewState.isEmployeeRoleView = false;
      }else {
        this.viewState.isLoadingOnPostRole = false;
      }

    }catch(error){
      this.viewState.isLoadingOnPostRole = false;
      console.log(error);
    }
  }

  cancelEmployeeEmploymentHistoryButtonClickEvent(): void {
    this.viewState.isEmployeeEmploymentHistoryView = true;
    this.viewState.contractSelectedFile = null;
    this.viewState.selectedFilesChanged = false;
    this.employeeEmploymentHistoryView(this.viewState.activatedParam); // in order to reset files
  };

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  employeeEmploymentHistoryFormValueChecker(obj): boolean {
    for (let key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] !== null && obj[key] !== "") {
          return false;
      }
    }
    if(this.viewState.selectedFilesChanged || this.viewState.contractSelectedFile || this.viewState.employeeEmploymentHistoryViewModel.contract?.name) {
      return false;
    }
    return true;
  }

  openEmployeeFormerPositionFormViewState(formerPosition?: FormerPositionModel): void {
    this.viewState.isFormerPositionFormViewStateOpened = true;
    if(formerPosition) {
      this.viewState.isFormerPositionFormEditViewState = true;
      this.employeeFormerPositionForm.setValue({
        formerPositionId: formerPosition.id,
        title: formerPosition.title,
        startDate: new Date(formerPosition.startDate),
        endDate: new Date(formerPosition.endDate),
      })
    }
  };

  closeEmployeeFormerPositionFormViewState(status: string): void {
    if(status === 'save') {
      this.saveEmployeeFormerPositionClickHandler();
    } else {
      this.viewState.isFormerPositionFormViewStateOpened = false;
      this.viewState.isFormerPositionFormEditViewState = false;
      this.employeeFormerPositionForm.reset();
    }
  };

  openEmployeeFormerPositionDeleteModal(formerPosition?: FormerPositionModel): void {
    this.viewState.isFormerPositionDeleteModalOpened = true;
    this.viewState.formerPositionToDelete = formerPosition;
  };

  closeEmployeeFormerPositionDeleteModal(status: string): void {
    if(status === 'delete') {
      this.deleteEmployeeFormerPositionClickHandler();
    } else {
      this.viewState.isFormerPositionDeleteModalOpened = false;
      this.viewState.formerPositionToDelete = null;
    }
  };

  async saveEmployeeFormerPositionClickHandler(): Promise<void> {
    if(!this.employeeFormerPositionForm.valid) {
      this.employeeFormerPositionForm.markAllAsTouched();
      return;
    }

    if(this.viewState.isFormerPositionFormEditViewState) {
      this.viewState.editEmployeeFormerPositionView = {
        employeeId: this.viewState.activatedParam,
        formerPositionId: this.employeeFormerPositionForm.value.formerPositionId,
        title: this.employeeFormerPositionForm.value.title,
        startDate: this.employeeFormerPositionForm.value.startDate,
        endDate: this.employeeFormerPositionForm.value.endDate
      }
    } else {
      this.viewState.addEmployeeFormerPositionView = {
        employeeId: this.viewState.activatedParam,
        title: this.employeeFormerPositionForm.value.title,
        startDate: this.employeeFormerPositionForm.value.startDate,
        endDate: this.employeeFormerPositionForm.value.endDate
      }
    }
    
    try {
      this.viewState.isLoadingOnPost = true;
      let response = null;
      if(this.viewState.isFormerPositionFormEditViewState) {
        response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditformerpositionPost(this.viewState.editEmployeeFormerPositionView));
      } else {
        response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddformerpositionPost(this.viewState.addEmployeeFormerPositionView));
      }
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isFormerPositionFormViewStateOpened = false;
        this.viewState.isFormerPositionFormEditViewState = false;
        this.viewState.selectedFilesChanged = false;
        this.employeeFormerPositionForm.reset();
        this.employeeEmploymentHistoryView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }

  };

  async deleteEmployeeFormerPositionClickHandler(): Promise<void> {
    this.viewState.deleteEmployeeFormerPositionView = {
      employeeId: this.viewState.activatedParam,
      formerPositionId: this.viewState.formerPositionToDelete.id
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeleteformerpositionPost(this.viewState.deleteEmployeeFormerPositionView));
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isFormerPositionDeleteModalOpened = false;
        this.viewState.formerPositionToDelete = null;
        this.employeeEmploymentHistoryView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }
  };

  employeeEmploymentHistoryFormFormForbiddenDates(control: FormControl): {[s: string]: boolean} {
    const startDate = control.parent?.controls?.['jobStartDate']?.value;
    const endDate = control.parent?.controls?.['contractExpirationDate']?.value;
    if(startDate && endDate && startDate >= endDate) {
      return {'dateIsForbidden': true};
    } 
    return null;
  } 

  formerPositionsFormForbiddenDates(control: FormControl): {[s: string]: boolean} {
    const startDate = control.parent?.controls?.['startDate'];
    const endDate = control.parent?.controls?.['endDate'];
    if(startDate?.value?.getFullYear() >= endDate?.value?.getFullYear()) {
      return {'dateIsForbidden': true};
    } 
    return null;
  }   

  employeeEmploymentHistoryFormDateControlsSubscribe(): void {
    this.addSubscription(this.employeeEmploymentHistoryForm.get('jobStartDate').valueChanges.subscribe((value) => {
      this.employeeEmploymentHistoryForm.get('contractExpirationDate')?.setErrors(null);
    }));
    this.addSubscription(this.employeeEmploymentHistoryForm.get('contractExpirationDate')?.valueChanges.subscribe((value) => {
      this.employeeEmploymentHistoryForm.get('jobStartDate')?.setErrors(null);
    }));
  }

  employeeFormerPositionFormDateControlsSubscribe(): void {
    this.addSubscription(this.employeeFormerPositionForm.get('endDate').valueChanges.subscribe((value) => {
      this.employeeFormerPositionForm.get('startDate')?.setErrors(null);
    }));
    this.addSubscription(this.employeeFormerPositionForm.get('startDate').valueChanges.subscribe((value) => {
      this.employeeFormerPositionForm.get('endDate')?.setErrors(null);
    }));
  }
  
  addSubscription(subscription: Subscription) {
    this.subscriptions.push(subscription);
  }

  unsubscribeAll() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
    this.subscriptions = [];
  }
   
  ngOnDestroy(): void {
    this.unsubscribeAll();
  }

}
