import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { SkillsAndLanguagesViewStateModel } from './skillsAndLanguagesViewStateModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { BASE_PATH, EmployeeProfileService, FileType } from 'src/app/shared/infrastructure/PortalHttpClient';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-employee-skills-and-languages-feature',
  templateUrl: './employee-skills-and-languages-feature.component.html',
  styleUrls: ['./employee-skills-and-languages-feature.component.scss']
})

export class EmployeeSkillsAndLanguagesFeatureComponent implements OnInit {  
  viewState: SkillsAndLanguagesViewStateModel = {
    activatedParam: null,
    basePath: '',
    employmentModuleResourceModel: {},
    employmentErrorResourceModel: {},
    isEmployeeSkillsViewState: true,
    isEmployeeLanguagesViewState: true,
    employeeSkillsAndLanguagesView: {},
    deleteEmployeeSkillModel: {},
    addEmployeeSkillModel: {},
    addEmployeeLanguageModel: {},
    deleteEmployeeLanguageModel: {},
    isLoadingOnGet: false,
    isLoadingOnPostForSkills: false,
    isLoadingOnPostForLanguages: false,
    fileTypes: FileType,
    isEmployeeResumeViewState: true,
    resumeSelectedFile: null,
    resumeFileExtensionOnUpload: '',
    selectedFilesChanged: false,
    actionPermissions: {},
    isMyProfile: false,
  }

  employeeSkillsForm = new FormGroup({
    skill: new FormControl('', [Validators.required]),
  })

  employeeLanguagesForm = new FormGroup({
    language: new FormControl('', [Validators.required]),
  })
  
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
    this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
  };

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.employmentErrorResourceModel = this.globalResourceService.resourceResponse.errorResources;
  }

  employeeSkillEditFormButtonClickEvent() {
    this.viewState.isEmployeeSkillsViewState = false;
  };

  employeeLanguageEditFormButtonClickEvent() {
    this.viewState.isEmployeeLanguagesViewState = false;
  };

  async employeeSkillsAngLanguagesView(id): Promise<void> {
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileSkillsandlanguageGet(id));
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.employeeSkillsAndLanguagesView = response.value;
        this.viewState.isMyProfile = response.value.isMyProfile;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async saveEmployeeSkillClickEvent(value): Promise<void> {
    this.viewState.addEmployeeSkillModel = {
      employeeId: this.viewState.activatedParam,
      skill: value.skill
    }

    if(!this.employeeSkillsForm.valid){
      this.employeeSkillsForm.markAllAsTouched();
      return;
    }
  
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddcomputerskillPost(this.viewState.addEmployeeSkillModel));
      if(response.ok) {
        this.employeeSkillsForm.reset();
        this.viewState.isEmployeeSkillsViewState = true;
        this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async saveEmployeeLanguageClickEvent(employeeLanguageValue): Promise<void> {
    this.viewState.addEmployeeLanguageModel = {
      employeeId: this.viewState.activatedParam,
      language: employeeLanguageValue.language,
    }

    if(!this.employeeLanguagesForm.valid){
      this.employeeLanguagesForm.markAllAsTouched();
      return;
    }
  
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileAddlanguagePost(this.viewState.addEmployeeLanguageModel));
      if(response.ok) {
        this.employeeLanguagesForm.reset();
        this.viewState.isEmployeeLanguagesViewState = true;
        this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async deleteEmployeeSkillButtonClickEvent(employeeSkillValue): Promise<void> {
    this.viewState.deleteEmployeeSkillModel = {
      employeeId:  this.viewState.activatedParam,
      skillId: employeeSkillValue.id,
    };
    try{
      this.viewState.isLoadingOnPostForSkills = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeletecomputerskillPost(this.viewState.deleteEmployeeSkillModel));
      if(response.ok) {
        this.viewState.isLoadingOnPostForSkills = false;
        this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPostForSkills = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnPostForSkills = false;
      console.log(error);
    }
  };

  async deleteEmployeeLanguageButtonClickEvent(employeeLanguageValue): Promise<void> {
    this.viewState.deleteEmployeeLanguageModel = {
      employeeId:  this.viewState.activatedParam,
      languageId: employeeLanguageValue.id,
    };
        
    try{
      this.viewState.isLoadingOnPostForLanguages = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileDeletelanguagePost(this.viewState.deleteEmployeeLanguageModel));
      if(response.ok) {
        this.viewState.isLoadingOnPostForLanguages = false;
        this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPostForLanguages = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnPostForLanguages = false;
      console.log(error);
    }
  };

  cancelEmployeeSkillsButtonClickEvent(): void {
    this.viewState.isEmployeeSkillsViewState = true;
    this.employeeSkillsForm.reset();
  };

  cancelEmployeeLanguagesButtonClickEvent(): void {
    this.viewState.isEmployeeLanguagesViewState = true;
    this.employeeLanguagesForm.reset();
  };

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  employeeResumeEditFormButtonClickEvent(): void {
    this.viewState.isEmployeeResumeViewState = false;
  }

  employeeResumeFileSelectedButtonCliekEvent(event): void {
    this.viewState.resumeSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.resumeFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.resumeFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  deleteBloodGroupFileButtonClickEvent(): void {
    this.viewState.resumeSelectedFile = null;
    this.viewState.employeeSkillsAndLanguagesView.resume = null;
    this.viewState.selectedFilesChanged = true;
  }
  
  async saveEmployeeResumeClickEvent(): Promise<void> {
    console.log('dzia')
      try {
        this.viewState.isLoadingOnGet = true;

        let saveEmployeeResumeFileEvent;
        if(this.viewState.resumeSelectedFile || (!this.viewState.resumeSelectedFile && !this.viewState.employeeSkillsAndLanguagesView.resume?.name)) {
          saveEmployeeResumeFileEvent = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileUploadresumePostForm(
            this.viewState.activatedParam,
            this.viewState.resumeSelectedFile,
            ));
          }
          if(!saveEmployeeResumeFileEvent?.errors) {
            this.viewState.isEmployeeResumeViewState = true;
            this.viewState.resumeSelectedFile = null;
            this.viewState.selectedFilesChanged = false;
            this.employeeSkillsAngLanguagesView(this.viewState.activatedParam);
          } else {
            this.viewState.isLoadingOnGet = false;
          }
  
      } catch (error) {
        this.viewState.isLoadingOnGet = false;
        console.error(error);
      }
    };

  cancelEmployeeResumeButtonClickEvent(): void {
    this.viewState.isEmployeeResumeViewState = true;
    this.viewState.resumeSelectedFile = null;
    this.viewState.selectedFilesChanged = false;
    this.employeeSkillsAngLanguagesView(this.viewState.activatedParam); // in order to reset files
  }

  employeeResumeValueChecker(): boolean {
    if(this.viewState.selectedFilesChanged || this.viewState.resumeSelectedFile || this.viewState.employeeSkillsAndLanguagesView.resume?.name) {
      return false;
    }
    return true;
  }
}
