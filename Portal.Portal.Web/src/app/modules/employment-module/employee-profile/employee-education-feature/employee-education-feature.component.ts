import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription, firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';

import { EmployeeEducationViewModel } from './employeeEducationViewModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { EmployeeProfileService, EmployeeTrainingModel, EmployeeUniversityModel } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-employee-education-feature',
  templateUrl: './employee-education-feature.component.html',
  styleUrls: ['./employee-education-feature.component.scss']
})
export class EmployeeEducationFeatureComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  
  viewState: EmployeeEducationViewModel = {
    activatedParam: null,
    employmentModuleResourceModel: {},
    employmentErrorResourceModel: {},
    employeeEducationViewModel: {},
    isTrainingFormViewStateOpened: false,
    isTrainingFormEditViewState: false,
    isTrainingDeleteModalOpened: false,
    trainingToDelete: null,
    // addEmployeeTrainingView: {},
    // editEmployeeTrainingView: {},
    deleteEmployeeTrainingView: {},
    isUniversityFormViewStateOpened: false,
    isUniversityFormEditViewState: false,
    isUniversityDeleteModalOpened: false,
    universityToDelete: null,
    // addEmployeeUniversityView: {},
    // editEmployeeUniversityView: {},
    deleteEmployeeUniversityView: {},
    isLoadingOnGet: false,
    isLoadingOnPost: false,
    actionPermissions: {},
    isMyProfile: false,
    certificateSelectedFile: null,
    certificateFileExtensionOnUpload: '',
    selectedFilesChanged: false,
  }

  employeeTrainingForm = new FormGroup({
    trainingId: new FormControl(null),
    training: new FormControl('', [Validators.required])
  });

  employeeUniversityForm = new FormGroup({
    universityId: new FormControl(null),
    university: new FormControl('', [Validators.required]),
    faculty: new FormControl('', [Validators.required]),
    startDate: new FormControl(null, [Validators.required, this.employeeUniversityFormForbiddenDates.bind(this)]),
    endDate: new FormControl(null, [Validators.required, this.employeeUniversityFormForbiddenDates.bind(this)])
  });

  getFileClassByExtension = getFileClassByExtension;
  
  constructor(
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private employeeProfileService: EmployeeProfileService,
  ) {
    this.viewState.activatedParam = +this.router.url.split('/')[4];
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;

  }
   
  ngOnInit(): void {
    this.loadResource();
    this.employeeEducationView(this.viewState.activatedParam);
    this.employeeUniversityFormDateControlsSubscribe();
  };
  
  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.employmentErrorResourceModel = this.globalResourceService.resourceResponse.errorResources;
  }

  async employeeEducationView(id: number): Promise<void> {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmployeeeducationGet(id));
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.employeeEducationViewModel = response.value;
        console.log(response.value)
        this.viewState.isMyProfile = response.value.isMyProfile;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error, 'error');
    }
  };

  openEmployeeTrainingFormViewState(training?: EmployeeTrainingModel): void {
    this.viewState.isTrainingFormViewStateOpened = true;
    if(training) {
      this.viewState.isTrainingFormEditViewState = true;
      this.employeeTrainingForm.setValue({
        training: training.training,
        trainingId: training.id
      })
    }
  };

  closeEmployeeTrainingFormViewState(status: string): void {
    if(status === 'save') {
      this.saveEmployeeTrainingClickHandler();
    } else {
      this.viewState.isTrainingFormViewStateOpened = false;
      this.viewState.isTrainingFormEditViewState = false;
      this.viewState.certificateSelectedFile = null;
      this.viewState.selectedFilesChanged = false;
      this.employeeTrainingForm.reset();
    }
  };

  openEmployeeTrainingDeleteModal(training?: EmployeeTrainingModel): void {
    this.viewState.isTrainingDeleteModalOpened = true;
    this.viewState.trainingToDelete = training;
  };

  closeEmployeeTrainingDeleteModal(status: string): void {
    if(status === 'delete') {
      this.deleteEmployeeTrainingClickHandler();
    } else {
      this.viewState.isTrainingDeleteModalOpened = false;
      this.viewState.trainingToDelete = null;
    }
  };

  employeeCertificateFileSelectedButtonCliekEvent(event): void {
    this.viewState.certificateSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.certificateFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.certificateFileExtensionOnUpload = '';
    }
    event.target.value = '';

    console.log(this.viewState.certificateFileExtensionOnUpload);
    console.log(this.viewState.certificateSelectedFile);
  }


  deleteCertificateFileButtonClickEvent(): void {
    this.viewState.certificateSelectedFile = null;
    // this.viewState.employeeEducationViewModel.cerificate = null;
    this.viewState.selectedFilesChanged = true;
  }

  async saveEmployeeTrainingClickHandler(): Promise<void> {
    if(!this.employeeTrainingForm.valid) {
      this.employeeTrainingForm.markAllAsTouched();
      return;
    }
    
    try {
      this.viewState.isLoadingOnPost = true;
      let response = null;
      if(this.viewState.isTrainingFormEditViewState) {
        response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemployeetrainingPostForm(
          this.viewState.activatedParam,
          this.employeeTrainingForm.value.trainingId,
          this.employeeTrainingForm.value.training,
          // this.viewState.certificateSelectedFile,
        ));
      } else {
        response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddemployeetrainingPostForm(
          this.viewState.activatedParam,
          this.employeeTrainingForm.value.training,
          this.viewState.certificateSelectedFile,
        ));
      }
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isTrainingFormViewStateOpened = false;
        this.viewState.isTrainingFormEditViewState = false;
        this.employeeTrainingForm.reset();
        this.employeeEducationView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }

  };
  
  async deleteEmployeeTrainingClickHandler(): Promise<void> {
    this.viewState.deleteEmployeeTrainingView = {
      employeeId: this.viewState.activatedParam,
      trainingId: this.viewState.trainingToDelete.id
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeleteemployeetrainingPost(this.viewState.deleteEmployeeTrainingView));
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isTrainingDeleteModalOpened = false;
        this.viewState.trainingToDelete = null;
        this.employeeEducationView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }
  };

  openEmployeeUniversityFormViewState(university?: EmployeeUniversityModel): void {
    this.viewState.isUniversityFormViewStateOpened = true;
    if(university) {
      this.viewState.isUniversityFormEditViewState = true;
      this.employeeUniversityForm.setValue({
        universityId: university.id,
        university: university.university,
        faculty: university.faculty,
        startDate: new Date(university.startDate),
        endDate: new Date(university.endDate),
      })
    }
  };

  closeEmployeeUniversityFormViewState(status: string): void {
    if(status === 'save') {
      this.saveEmployeeUniversityClickHandler();
    } else {
      this.viewState.isUniversityFormViewStateOpened = false;
      this.viewState.isUniversityFormEditViewState = false;
      this.employeeUniversityForm.reset();
    }
  };

  openEmployeeUniversityDeleteModal(university?: EmployeeUniversityModel): void {
    this.viewState.isUniversityDeleteModalOpened = true;
    this.viewState.universityToDelete = university;
  };

  closeEmployeeUniversityDeleteModal(status: string): void {
    if(status === 'delete') {
      this.deleteEmployeeUniversityClickHandler();
    } else {
      this.viewState.isUniversityDeleteModalOpened = false;
      this.viewState.universityToDelete = null;
    }
  };

  async saveEmployeeUniversityClickHandler(): Promise<void> {
    if(!this.employeeUniversityForm.valid) {
      this.employeeUniversityForm.markAllAsTouched();
      return;
    }

    if(this.viewState.isUniversityFormEditViewState) {
      // this.viewState.editEmployeeUniversityView = {
      //   employeeId: this.viewState.activatedParam,
      //   universityId: this.employeeUniversityForm.value.universityId,
      //   university: this.employeeUniversityForm.value.university,
      //   faculty: this.employeeUniversityForm.value.faculty,
      //   startDate: this.employeeUniversityForm.value.startDate,
      //   endDate: this.employeeUniversityForm.value.endDate
      // }
    } else {
      // this.viewState.addEmployeeUniversityView = {
      //   employeeId: this.viewState.activatedParam,
      //   university: this.employeeUniversityForm.value.university,
      //   faculty: this.employeeUniversityForm.value.faculty,
      //   startDate: this.employeeUniversityForm.value.startDate,
      //   endDate: this.employeeUniversityForm.value.endDate
      // }
    }
    
    try {
      this.viewState.isLoadingOnPost = true;
      let response = null;
      // if(this.viewState.isUniversityFormEditViewState) {
      //   response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemployeeuniversityPost(this.viewState.editEmployeeUniversityView));
      // } else {
      //   response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddemployeeuniversityPost(this.viewState.addEmployeeUniversityView));
      // }
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isUniversityFormViewStateOpened = false;
        this.viewState.isUniversityFormEditViewState = false;
        this.employeeUniversityForm.reset();
        this.employeeEducationView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }

  };

  async deleteEmployeeUniversityClickHandler(): Promise<void> {
    this.viewState.deleteEmployeeUniversityView = {
      employeeId: this.viewState.activatedParam,
      universityId: this.viewState.universityToDelete.id
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeleteemployeeuniversityPost(this.viewState.deleteEmployeeUniversityView));
      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isUniversityDeleteModalOpened = false;
        this.viewState.universityToDelete = null;
        this.employeeEducationView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnPost = false;
      console.error(error);
    }
  };

  employeeUniversityFormForbiddenDates(control: FormControl): {[s: string]: boolean} {
    const startDate = control.parent?.controls?.['startDate'];
    const endDate = control.parent?.controls?.['endDate'];
    if(startDate?.value?.getFullYear() >= endDate?.value?.getFullYear()) {
      return {'dateIsForbidden': true};
    } 
    return null;
  }  

  employeeUniversityFormDateControlsSubscribe() {
    this.addSubscription(this.employeeUniversityForm.get('endDate').valueChanges.subscribe((value) => {
      this.employeeUniversityForm.get('startDate')?.setErrors(null);
    }));
    this.addSubscription(this.employeeUniversityForm.get('startDate').valueChanges.subscribe((value) => {
      this.employeeUniversityForm.get('endDate')?.setErrors(null);
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
