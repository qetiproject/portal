import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { SelectEvent } from '@progress/kendo-angular-layout';
import { enumToArray } from 'src/app/shared/infrastructure/Utils/utils';
import { EmployeePersonalInformationViewModel } from './employeePersonalInformationViewModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { EventBus, OkButtonClickEvent, UpdateUserPhotoButtonClickEvent } from 'src/app/shared/infrastructure/CustomApi/event.bus';
import { BASE_PATH, EmployeeProfileService, EmployeeStatus, HideEmployeeProfilePhotoRequest, JobType } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-employee-personal-information-feature',
  templateUrl: './employee-personal-information-feature.component.html',
  styleUrls: ['./employee-personal-information-feature.component.scss'],
})
export class EmployeePersonalInformationFeatureComponent implements OnInit {
  viewState: EmployeePersonalInformationViewModel = {
    isEmployeePersonalInformationView: true,
    employmentModuleResourceModel: {},
    employeePersonalInformationView: {},
    employeeStatus: EmployeeStatus,
    activatedParam: null,
    breadItems: [],
    jobTypes: null,
    jobTypesEnum: JobType,
    employeeSecondaryInformationTabItems: [],
    employeeSecondaryInformationActiveTab: '',
    selectedFile: null,
    basePath: '/',
    errorResourceList: {},
    statuses: null,
    isOpenedProfilePhotoDialog: false,
    selectedItemIndex: null,
    updateEmployeePersonalInformationModel: {},
    employeeListUrl: [],
    subscriptions:  [],
    isLoadingOnGet: false,
    isLoadingOnPost: false,
    actionPermissions: {},
    isHideProfilePhoto: null,
  }
  
  employeePersonalInformationForm = new FormGroup({
    fullName: new FormControl('', [Validators.required]),
    position: new FormControl('', [Validators.required]),
    jobType: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    dateOfBirth: new FormControl(new Date(), [Validators.required]),
    personalId: new FormControl('', [Validators.required]),
    status: new FormControl('', [Validators.required])
  });

  constructor(
    private globalResourceService: GlobalResourceService,
    private activatedRoute: ActivatedRoute,
    private employeeProfileService: EmployeeProfileService,
    private router: Router,
    private eventBus: EventBus,
    @Inject(BASE_PATH) basePath: string
  ) {
    if (basePath) {
      this.viewState.basePath = `${basePath}/`
    }
    this.viewState.activatedParam = this.activatedRoute.snapshot.params['id'];
    this.viewState.employeeSecondaryInformationActiveTab = this.router.url.split('/')[5];
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  }

  ngOnInit(): void {
    this.loadResource();
    this.employeePersonalInformationView(this.viewState.activatedParam);
    this.subscribe();
  };

   loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.errorResourceList = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.jobTypes = enumToArray(JobType, this.viewState.employmentModuleResourceModel);
    this.viewState.statuses = enumToArray(EmployeeStatus, this.viewState.employmentModuleResourceModel);
    this.viewState.breadItems = [
      {
        text: this.viewState.employmentModuleResourceModel.employeeList,
        url: 'management/employee-service'
      },
      {
        text: this.viewState.employmentModuleResourceModel.employeeProfile
      },
    ];
    this.viewState.employeeSecondaryInformationTabItems = [
      {title:  this.viewState.employmentModuleResourceModel.jobInfo, path: "job-info"}, 
      {title: this.viewState.employmentModuleResourceModel.education, path: "education"}, 
      {title: this.viewState.employmentModuleResourceModel.payrollBasic, path: "payroll-basic"}, 
      {title: this.viewState.employmentModuleResourceModel.employmentHistory, path: "employment-history"}, 
      {title: this.viewState.employmentModuleResourceModel.skillsAndLanguages, path: "skills-and-languages"}, 
      {title: this.viewState.employmentModuleResourceModel.otherInfo, path: "other-info"}
    ];
  }
  
  breadItemsViewState(item: any): void {
    this.viewState.selectedItemIndex = this.viewState.breadItems.findIndex((i) => i.text === item.text);
    this.viewState.employeeListUrl = this.viewState.breadItems
      .slice(0, this.viewState.selectedItemIndex + 1)
      .map((i) => i.url);
    this.router.navigate(this.viewState.employeeListUrl);
  };

  employeeSecondaryInformationTabSelectViewState(e: SelectEvent): void {
    this.router.navigate([`${this.viewState.employeeSecondaryInformationTabItems[e.index].path}`], {relativeTo: this.activatedRoute});
  };

  async employeePersonalInformationView(id: number) {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmployeepersonalinformationGet(id));
      if(response.ok) {
        this.viewState.employeePersonalInformationView = response.value;
        this.viewState.isHideProfilePhoto = response.value.hidden;
        if(response.value.photo != '') {
          this.viewState.employeePersonalInformationView.photo = `${this.viewState.basePath}${response.value.photo}`;
        } else {
          this.viewState.employeePersonalInformationView.photo = '';
        }
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error, 'error')
    }
  };

  async hideEmployeeProfilePhotoEvent(): Promise<void> {
    
    this.viewState.isHideProfilePhoto = !this.viewState.isHideProfilePhoto;

    let hideEmployeeProfilePhotoRequest: HideEmployeeProfilePhotoRequest = {
      id: this.viewState.activatedParam,
      hidden: this.viewState.isHideProfilePhoto,
    }

    try{
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileHideemployeeprofilephotoPost(hideEmployeeProfilePhotoRequest));
    }catch(error) {
      console.log(error);
    }
  }

  async employeeProfilePhotoSelectedButtonCliekEvent(event): Promise<void> {
    this.viewState.selectedFile = event.target.files[0];
    try {
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUpdateemployeeprofilephotoPostForm(this.viewState.employeePersonalInformationView.id, this.viewState.selectedFile));
      if(response.ok) {
        if(JSON.parse(localStorage.getItem('user')).employeeId === this.viewState.employeePersonalInformationView.id) {
          this.eventBus.send(new UpdateUserPhotoButtonClickEvent());
        }
        this.employeePersonalInformationView(this.viewState.activatedParam);
      }
    } catch(error) {
      console.log(error, 'error')
    }

  };

  employeePersonalInformationEditButtonClickEvent() {
    this.viewState.isEmployeePersonalInformationView = false;
    this.employeePersonalInformationForm.setValue({
      fullName: this.viewState.employeePersonalInformationView.fullName,
      position: this.viewState.employeePersonalInformationView.position,
      phoneNumber: this.viewState.employeePersonalInformationView.phoneNumber,
      email: this.viewState.employeePersonalInformationView.email,
      dateOfBirth: new Date(this.viewState.employeePersonalInformationView.dateOfBirth),
      personalId: this.viewState.employeePersonalInformationView.personalId,
      status: this.viewState.employeePersonalInformationView.status,
      jobType: this.viewState.employeePersonalInformationView.jobType,
    })
  };

  closeProfilePhotoDialogButtonCliekEvent(): void {
    this.viewState.isOpenedProfilePhotoDialog = false;
  };

  openProfilePhotoDialogButtonClickEvent(): void {
    this.viewState.isOpenedProfilePhotoDialog = true;
  };

  async saveEmployeePersonalInformationButtonCliclEvent(employeePersonalInformationFormValue) {
    this.viewState.updateEmployeePersonalInformationModel = {
      id: this.viewState.employeePersonalInformationView.id,
      fullName: employeePersonalInformationFormValue.fullName,
      position: employeePersonalInformationFormValue.position,
      personalId: employeePersonalInformationFormValue.personalId,
      dateOfBirth: employeePersonalInformationFormValue.dateOfBirth,
      phoneNumber: employeePersonalInformationFormValue.phoneNumber,
      email: employeePersonalInformationFormValue.email,
      status: employeePersonalInformationFormValue.status,
      jobType: employeePersonalInformationFormValue.jobType,
    };
    
    if (!this.employeePersonalInformationForm.valid) {
      return;
    } 

    try{
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemployeepersonalinformationPost(this.viewState.updateEmployeePersonalInformationModel));

      if (response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.eventBus.send(new OkButtonClickEvent());
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }

  };

  async deleteEmployeeProfilePhotoButtonClickEvent(id: number) {
    this.viewState.isOpenedProfilePhotoDialog = false;
    try {
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeleteemployeeprofilephotoPostForm(id));
      if(response.ok) {
        if(JSON.parse(localStorage.getItem('user')).employeeId === this.viewState.employeePersonalInformationView.id) {
          this.eventBus.send(new UpdateUserPhotoButtonClickEvent());
        }
        this.employeePersonalInformationView(this.viewState.activatedParam);
      }
    } catch(error) {
      console.log(error, 'error')
    }
  };

  okButtonClickEventHandler = (message: OkButtonClickEvent): void => {
    this.viewState.isEmployeePersonalInformationView = true;
    this.employeePersonalInformationView(this.viewState.activatedParam);
  };

  cancelEmployeePersonalInformationButtonClickEvent() {
    this.viewState.isEmployeePersonalInformationView = true;
  };

  subscribe() {
   this.viewState.subscriptions = [
      this.eventBus.subscribe<OkButtonClickEvent>(OkButtonClickEvent, this.okButtonClickEventHandler)
    ];
  };

  unSubscribe() {
    this.viewState.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

}
