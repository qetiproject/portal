import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

import { EmployeeJobInfoViewStateModel } from './employeeJobInfoiewModel';

import { BASE_PATH, EmployeeProfileService, FileType, ResourceService } from 'src/app/shared/infrastructure/PortalHttpClient';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-employee-job-info-feature',
  templateUrl: './employee-job-info-feature.component.html',
  styleUrls: ['./employee-job-info-feature.component.scss']
})
export class EmployeeJobInfoFeatureComponent implements OnInit {
  viewState: EmployeeJobInfoViewStateModel = {
    activatedParam: null,
    basePath: '',
    isEmployeeJobInfoView: true,
    employmentModuleResourceModel: {},
    employeeJobInfoViewModel: {},
    timeZonesResourceModel: {},
    editEmployeeJobInfoViewModel: {},
    fileTypes: FileType,
    idDocumentSelectedFile: null,
    idDocumentFileExtensionOnUpload: '',
    isLoading: false,
    selectedFilesChanged: false,
    actionPermissions: {},
  }

  employeeJobInfoForm = new FormGroup({
    region: new FormControl(''),
    workAddress: new FormControl(''),
    idExpirationDate: new FormControl(null),
    department: new FormControl(''),
    timeZone: new FormControl(null),
  });

  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private employeeProfileService: EmployeeProfileService,
    @Inject(BASE_PATH) basePath: string,
  ) {
    this.viewState.activatedParam = +this.router.url.split('/')[4];
    this.viewState.basePath = `${basePath}`;
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  };

  ngOnInit(): void {
    this.loadResource();
    this.employeeProfileTimezonesData();
    this.employeeJobInfoView(this.viewState.activatedParam);
  };

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
  }

  async employeeJobInfoView(id: number): Promise<void> {
    try {
      this.viewState.isLoading = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmployeejobinfoGet(id));
      if(response.ok) {
        this.viewState.isLoading = false;
        this.viewState.employeeJobInfoViewModel = response.value;
      } else {
        this.viewState.isLoading = false;
      }
    } catch(error) {
      this.viewState.isLoading = false;
      console.log(error, 'error');
    }
  };

  async employeeProfileTimezonesData(): Promise<void> {
    try {
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileTimezonesGet());
      if(response) {
        this.viewState.timeZonesResourceModel = response;
      }
    } catch(error) {
      console.log(error, 'error');
    }
  };
  
  employeeJobInfoEditButtonClickEvent(): void {
    this.viewState.isEmployeeJobInfoView = false;
    this.employeeJobInfoForm.setValue({
      region: this.viewState.employeeJobInfoViewModel.region,
      workAddress: this.viewState.employeeJobInfoViewModel.workAddress,
      idExpirationDate: this.viewState.employeeJobInfoViewModel.idExpirationDate ? new Date(this.viewState.employeeJobInfoViewModel.idExpirationDate) : null,
      department: this.viewState.employeeJobInfoViewModel.department,
      timeZone: this.viewState.employeeJobInfoViewModel.timeZone.key === "" ? this.viewState.timeZonesResourceModel.defaultTimeZone : this.viewState.employeeJobInfoViewModel.timeZone
    });
  };

  employeeJobInfoIdDocumentFileSelectedButtonCliekEvent(event): void {
    this.viewState.idDocumentSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.idDocumentFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.idDocumentFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }


  deleteIdDocumentFileButtonClickEvent(): void {
    this.viewState.idDocumentSelectedFile = null;
    this.viewState.employeeJobInfoViewModel.idDocument = null;
    this.viewState.selectedFilesChanged = true;
    }

  async saveEmployeeJobInfoButtonClickEvent(employeeJobInfoFormValue): Promise<void> {
    this.viewState.editEmployeeJobInfoViewModel = {
      id: this.viewState.activatedParam,
      region: employeeJobInfoFormValue.region,
      workAddress: employeeJobInfoFormValue.workAddress,
      idExpirationDate: employeeJobInfoFormValue.idExpirationDate,
      department: employeeJobInfoFormValue.department,
      timeZone: employeeJobInfoFormValue.timeZone['key']
    }
    
    try {
      this.viewState.isLoading = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemployeejobinfoPost(this.viewState.editEmployeeJobInfoViewModel));

      let saveEmployeeIdDocumentFileEvent;
      if(this.viewState.idDocumentSelectedFile || (!this.viewState.idDocumentSelectedFile && !this.viewState.employeeJobInfoViewModel.idDocument?.name)) {
        saveEmployeeIdDocumentFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadiddocumentPostForm(
          this.viewState.activatedParam,
          this.viewState.idDocumentSelectedFile,
          ));
        }

      Promise.all([response, saveEmployeeIdDocumentFileEvent]).then((result) => {
        if(response.ok && !saveEmployeeIdDocumentFileEvent?.errors) {
          this.viewState.isEmployeeJobInfoView = true;
          this.viewState.idDocumentSelectedFile = null;
          this.viewState.selectedFilesChanged = false;
          this.employeeJobInfoView(this.viewState.activatedParam);
        } else {
          this.viewState.isLoading = false;
        }
      });
    } catch (error) {
      this.viewState.isLoading = false;
      console.error(error);
    }
  };

  cancelEmployeeJobInfoButtonClickEvent(): void {
    this.viewState.isEmployeeJobInfoView = true;
    this.viewState.idDocumentSelectedFile = null;
    this.viewState.selectedFilesChanged = false;
    this.employeeJobInfoView(this.viewState.activatedParam);
  };

  employeeJobInfoFormValueChecker(obj): boolean {
    for (let key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] !== null && obj[key] !== "" && obj[key] !== this.viewState.timeZonesResourceModel.defaultTimeZone) {
          return false;
      }
    }
    if(this.viewState.idDocumentSelectedFile || this.viewState.employeeJobInfoViewModel.idDocument?.name) {
      return false;
    }
    return true;
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }
}
