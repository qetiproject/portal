import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { EmployeeOtherInfoViewStateModel } from './employeeOtherInfoViewStateModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { BASE_PATH, Conviction, EmployeeProfileService, FileType, Gender, MaritalStatus } from 'src/app/shared/infrastructure/PortalHttpClient';
import { assignFileTypeForUpload, enumToArray, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-employee-other-info-feature',
  templateUrl: './employee-other-info-feature.component.html',
  styleUrls: ['./employee-other-info-feature.component.scss']
})
export class EmployeeOtherInfoFeatureComponent implements OnInit {
  viewState: EmployeeOtherInfoViewStateModel = {
    employmentModuleResourceModel: {},
    employmentErrorResourceModel: {},
    activatedParam: null,
    isEmployeeOtherInfoViewState: true,
    employeeOtherInfoViewModel: {}, 
    employeeEmergencyContactViewModel: {},
    genders: null,
    maritalStatuses: null,
    convictions: null,
    bloodGroupSelectedFile: null,
    bloodGroupFileExtensionOnUpload: '',
    medicalCertificateSelectedFile: null,
    medicalCertificateFileExtensionOnUpload: '',
    alergiesSelectedFile: null,
    alergiesFileExtensionOnUpload: '',
    drivingLicenseSelectedFile: null,
    drivingLicenseFileExtensionOnUpload: '',
    updateEmployeeOtherInfoForm: {},
    basePath: '',
    isFilesUpload: false,
    fileTypes: FileType,
    isLoading: false,
    selectedFilesChanged: false,
    actionPermissions: {},
  }

  employeeOtherInfoForm = new FormGroup({
    gender: new FormControl(''),
    maritalStatus: new FormControl(''),
    legalAddress: new FormControl(''),
    actualAddress: new FormControl(''),
    conviction: new FormControl(''),
    fullName: new FormControl(''),
    relationship: new FormControl(''),
    phoneNumber: new FormControl(''),
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
  }

  ngOnInit(): void {
    this.loadResource();
    this.employeeOtherInfoView(this.viewState.activatedParam);
  }

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.employmentErrorResourceModel = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.maritalStatuses = enumToArray(MaritalStatus, this.viewState.employmentModuleResourceModel);
    this.viewState.genders = enumToArray(Gender, this.viewState.employmentModuleResourceModel);
    this.viewState.convictions = enumToArray(Conviction, this.viewState.employmentModuleResourceModel);
  }


  async employeeOtherInfoView(id: number): Promise<void> {
    try{
      this.viewState.isLoading = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileOtherinformationGet(id));
      if(response) {
        this.viewState.isLoading = false;
        this.viewState.employeeOtherInfoViewModel = response.value.otherInformation;
        this.viewState.employeeEmergencyContactViewModel = response.value.emergencyContact;
      } else {
        this.viewState.isLoading = false;
      }
    }catch(error){
      this.viewState.isLoading = false;
      console.log(error);
    }

  }

  employeeOtherInfoEditFormButtonClickEvent(): void {
    this.viewState.isEmployeeOtherInfoViewState = false;
    this.employeeOtherInfoForm.setValue({
      gender: this.viewState.employeeOtherInfoViewModel.gender,
      maritalStatus: this.viewState.employeeOtherInfoViewModel.maritalStatus,
      legalAddress: this.viewState.employeeOtherInfoViewModel.legalAddress,
      actualAddress: this.viewState.employeeOtherInfoViewModel.actualAddress,
      conviction: this.viewState.employeeOtherInfoViewModel.conviction,
      fullName: this.viewState.employeeEmergencyContactViewModel.fullName,
      relationship: this.viewState.employeeEmergencyContactViewModel.relationship,
      phoneNumber: this.viewState.employeeEmergencyContactViewModel.phoneNumber,
    })
  }

  employeeOtherInfoBloodGroupFileSelectedButtonCliekEvent(event): void {
    this.viewState.bloodGroupSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.bloodGroupFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.bloodGroupFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  employeeOtherInfoMedicalCertificateFileSelectedButtonCliekEvent(event): void {
    this.viewState.medicalCertificateSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.medicalCertificateFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.medicalCertificateFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  employeeOtherInfoAlergiesFileSelectedButtonClickEvent(event): void {
    this.viewState.alergiesSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.alergiesFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.alergiesFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  employeeOtherInfoDrivingLicenceFileSelectedButtonCliekEvent(event): void {
    this.viewState.drivingLicenseSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.drivingLicenseFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.drivingLicenseFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  async saveEmployeeOtherInfoClickEvent(employeeOtherInfoFormValue): Promise<void> {
    this.viewState.updateEmployeeOtherInfoForm = {
      employeeId: this.viewState.activatedParam,
      gender: employeeOtherInfoFormValue.gender,
      maritalStatus: employeeOtherInfoFormValue.maritalStatus,
      legalAddress: employeeOtherInfoFormValue.legalAddress,
      actualAddress: employeeOtherInfoFormValue.actualAddress,
      conviction: employeeOtherInfoFormValue.conviction,
      fullName: employeeOtherInfoFormValue.fullName,
      relationship: employeeOtherInfoFormValue.relationship,
      phoneNumber: employeeOtherInfoFormValue.phoneNumber
    }

    try {
      this.viewState.isLoading = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditotherinformationPost(this.viewState.updateEmployeeOtherInfoForm));

      let saveEmployeeBloodGroupFileEvent;
      if(this.viewState.bloodGroupSelectedFile || (!this.viewState.bloodGroupSelectedFile && !this.viewState.employeeOtherInfoViewModel.bloodGroupAndRhesus?.name)) {
         saveEmployeeBloodGroupFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadbloodgroupandrhesusPostForm(
          this.viewState.activatedParam,
          this.viewState.bloodGroupSelectedFile,
          ));
        }

      let saveEmployeeMedicalCertificateFileEvent;
      if(this.viewState.medicalCertificateSelectedFile || (!this.viewState.medicalCertificateSelectedFile && !this.viewState.employeeOtherInfoViewModel.medicalCertificate?.name)) {
        saveEmployeeMedicalCertificateFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadmedicalcertificatePostForm(
          this.viewState.activatedParam,
          this.viewState.medicalCertificateSelectedFile,
          ));
        }

      let saveEmployeeAlergiesFileEvent;
      if(this.viewState.alergiesSelectedFile || (!this.viewState.alergiesSelectedFile && !this.viewState.employeeOtherInfoViewModel.alergies?.name)) {
         saveEmployeeAlergiesFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadalergiesPostForm(
          this.viewState.activatedParam,
          this.viewState.alergiesSelectedFile,
        ));
      }

      let saveEmployeeDrivingLicenseFileEvent;
      if(this.viewState.drivingLicenseSelectedFile || (!this.viewState.drivingLicenseSelectedFile && !this.viewState.employeeOtherInfoViewModel.drivingLicense?.name)) {
        saveEmployeeDrivingLicenseFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploaddrivinglicensePostForm(
          this.viewState.activatedParam,
          this.viewState.drivingLicenseSelectedFile,
          ));
        }

        Promise.all([response, saveEmployeeBloodGroupFileEvent, saveEmployeeMedicalCertificateFileEvent, saveEmployeeAlergiesFileEvent, saveEmployeeDrivingLicenseFileEvent]).then((result) => {
          if(response.ok && !saveEmployeeBloodGroupFileEvent?.errors && !saveEmployeeMedicalCertificateFileEvent?.errors && !saveEmployeeAlergiesFileEvent?.errors && !saveEmployeeDrivingLicenseFileEvent?.errors) {
            this.viewState.isEmployeeOtherInfoViewState = true;
            this.viewState.bloodGroupSelectedFile = null;
            this.viewState.medicalCertificateSelectedFile = null;
            this.viewState.alergiesSelectedFile = null;
            this.viewState.drivingLicenseSelectedFile = null;
            this.viewState.selectedFilesChanged = false;
            this.employeeOtherInfoView(this.viewState.activatedParam);
          } else {
            this.viewState.isLoading = false;
          }
        });

    }catch(error) {
      this.viewState.isLoading = false;
      console.log(error);
    }
  }

 deleteBloodGroupFileButtonClickEvent(): void {
  this.viewState.bloodGroupSelectedFile = null;
  this.viewState.employeeOtherInfoViewModel.bloodGroupAndRhesus = null;
  this.viewState.selectedFilesChanged = true;
  }

  deleteMedicalCertificateFileButtonClickEvent(): void {
    this.viewState.medicalCertificateSelectedFile = null;
    this.viewState.employeeOtherInfoViewModel.medicalCertificate = null;
    this.viewState.selectedFilesChanged = true;
  }

  deleteAlergiesFileButtonClickEvent(): void {
    this.viewState.alergiesSelectedFile = null;
    this.viewState.employeeOtherInfoViewModel.alergies = null;
    this.viewState.selectedFilesChanged = true;
  }

  deleteDrivingLicenseFileButtonClickEvent(): void {
    this.viewState.drivingLicenseSelectedFile = null;
    this.viewState.employeeOtherInfoViewModel.drivingLicense = null;
    this.viewState.selectedFilesChanged = true;
  }

  cancelEmployeeOtherInfoButtonClickEvent(): void {
    this.viewState.isEmployeeOtherInfoViewState = true;
    this.viewState.bloodGroupSelectedFile = null;
    this.viewState.medicalCertificateSelectedFile = null;
    this.viewState.alergiesSelectedFile = null;
    this.viewState.drivingLicenseSelectedFile = null;
    this.employeeOtherInfoView(this.viewState.activatedParam); // in order to reset files
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  employeeOtherInfoFormValueChecker(obj): boolean {
    for (let key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] !== null && obj[key] !== "") {
          return false;
      }
    }
    if(this.viewState.selectedFilesChanged || this.viewState.bloodGroupSelectedFile || this.viewState.medicalCertificateSelectedFile || this.viewState.alergiesSelectedFile || this.viewState.drivingLicenseSelectedFile || this.viewState.employeeOtherInfoViewModel.bloodGroupAndRhesus?.name || this.viewState.employeeOtherInfoViewModel.medicalCertificate?.name || this.viewState.employeeOtherInfoViewModel.alergies?.name || this.viewState.employeeOtherInfoViewModel.drivingLicense?.name) {
      return false;
    }
    return true;
  }
  
}
