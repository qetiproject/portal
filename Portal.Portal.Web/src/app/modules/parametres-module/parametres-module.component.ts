import { FormControl, FormGroup, Validators, } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { BASE_PATH, ParametresService, ResourceResponse, UpdateTimeOffRequestPanelRequests } from 'src/app/shared/infrastructure/PortalHttpClient';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';

import { ParametreViewStateModel } from './parametreViewStateModel';

@Component({
  selector: 'app-parametre-module',
  templateUrl: './parametres-module.component.html',
  styleUrls: ['./parametres-module.component.scss']
})
export class ParametreModuleComponent implements OnInit{
  
  layoutResource: ResourceResponse;
  
  viewState: ParametreViewStateModel = {
    isContractTypeViewState: true,
    isLoadingOnGet: false,
    contractTypesListModel: [],
    isLoadingOnPostForContractType: false,
    isLoadingOnPost: false,
    basePath: '',
    isGroupsViewState: true,
    isLoadingOnPostForGroup: false,
    groupsListModel: [],
    isTimeOffRequestsPanelViewState: true,
    hrWhoReceivesTimeOffRequests: '',
    hrWhoReceivesTimeOffRequestList: []
  }

  contractTypeForm = new FormGroup({
    contractType: new FormControl('', [Validators.required]),
  })

  groupForm = new FormGroup({
    group: new FormControl('', Validators.required),
  })

  timeOffRequestsPanelForm = new FormGroup({
    hrWhoReceivesTimeOffRequests: new FormControl('')
  })

  constructor(
    private globalResourceService: GlobalResourceService,
    private parametreHttpClient: ParametresService,
    @Inject(BASE_PATH) basePath: string,
  ) {
    this.viewState.basePath = `${basePath}`;
  }

  ngOnInit(): void {
    this.loadResource();
    this.employeeServiceContractTypesView();
    this.timeOffRequestsPanelView();
    this.nonCompilanceModuleGroupsView();
  }

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
  }

  contractTypeEditFormButtonClickEvent(): void {
    this.viewState.isContractTypeViewState = false;
  }

  async employeeServiceContractTypesView(): Promise<void> {
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresContracttypeslistGet());
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.contractTypesListModel = response.value.contractTypes;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async saveContractTypeButtonClickEvent(contractTypeFormValue): Promise<void> {

    if(!this.contractTypeForm.valid){
      this.contractTypeForm.markAllAsTouched();
      return;
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresCreatecontracttypePost(contractTypeFormValue.contractType));

      if(response.ok) {
        this.contractTypeForm.reset();
        this.viewState.isContractTypeViewState = true;
        this.employeeServiceContractTypesView();
        this.viewState.isLoadingOnPost = false;
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    }catch(error){
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
  }

  async deleteContractTypeButtonClickEvent(contractTypeValue): Promise<void> {

    try{
      this.viewState.isLoadingOnPostForContractType = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresDeletecontracttypePost(contractTypeValue.id));
      if(response.ok) {
        this.viewState.isLoadingOnPostForContractType = false;
        this.employeeServiceContractTypesView()
      } else {
        this.viewState.isLoadingOnPostForContractType = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnPostForContractType = false;
      console.log(error);
    }
  }

  async employeeDataView(data: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresEmployeesGet(data));

      if(response.ok) {
        this.viewState.hrWhoReceivesTimeOffRequestList = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  };

  hrListFilterEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.employeeDataView(inputValue);
  }

  async downloadmployeesExcelImportFileButtonClickEvent(): Promise<void> {

    try{
      const fileUrl = `${this.viewState.basePath}/api/adminpanel/downloademployeeimportfile`;
      window.open(fileUrl, '_blank');
    }catch(error){
      console.log(error);
    }
  }

  cancelContractTypeButtonClickEvent(): void {
    this.viewState.isContractTypeViewState = true;
    this.contractTypeForm.reset();
  }

  groupEditFormButtonClickEvent(): void {
    this.viewState.isGroupsViewState = false;
  }

  async nonCompilanceModuleGroupsView(): Promise<void> {
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresGroupslistGet());
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.groupsListModel = response.value.groups;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async saveGroupButtonClickEvent(groupFormValue): Promise<void> {

    if(!this.groupForm.valid){
      this.groupForm.markAllAsTouched();
      return;
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresCreategroupPost(groupFormValue.group));

      if(response.ok) {
        this.groupForm.reset();
        this.viewState.isGroupsViewState = true;
        this.nonCompilanceModuleGroupsView();
        this.viewState.isLoadingOnPost = false;
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    }catch(error){
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
  }

  async deleteGroupButtonClickEvent(groupValue): Promise<void> {

    try{
      this.viewState.isLoadingOnPostForGroup = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresDeletegroupPost(groupValue.id));
      if(response.ok) {
        this.viewState.isLoadingOnPostForGroup = false;
        this.nonCompilanceModuleGroupsView();
      } else {
        this.viewState.isLoadingOnPostForGroup = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnPostForGroup = false;
      console.log(error);
    }
  }

  cancelGroupButtonClickEvent() {
    this.viewState.isGroupsViewState = true;
    this.groupForm.reset();
  }

  timeOffRequestsPanelFormEditButtonClickEvent(): void {
    this.viewState.isTimeOffRequestsPanelViewState = false;
  }

  async timeOffRequestsPanelView(): Promise<void> {
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresGettimeoffrequestpanelGet());
      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.hrWhoReceivesTimeOffRequests = response.value.hrWhoReceivesTimeOffRequests;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async saveTimeOffRequestsPanelFormButtonClickEvent(timeOffRequestsPanelFormValue): Promise<void> {
    if(!this.timeOffRequestsPanelForm.valid){
      this.timeOffRequestsPanelForm.markAllAsTouched();
      return;
    }

    const updateTimeOffRequestPanelRequest: UpdateTimeOffRequestPanelRequests = {
      hrWhoReceivesTimeOffRequests: timeOffRequestsPanelFormValue.hrWhoReceivesTimeOffRequests
    }
    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.parametreHttpClient.apiParametresUpdatetimeoffrequestpanelPost(updateTimeOffRequestPanelRequest));

      if(response.ok) {
        this.timeOffRequestsPanelForm.reset();
        this.viewState.isTimeOffRequestsPanelViewState = true;
        this.timeOffRequestsPanelView();
        this.viewState.isLoadingOnPost = false;
      } else {
        this.viewState.isLoadingOnPost = false;
      }
    }catch(error){
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
  }
  
  cancelTimeOffRequestsPanelFormButtonClickEvent(): void {
    this.viewState.isTimeOffRequestsPanelViewState = true;
    this.timeOffRequestsPanelForm.reset();
  }

}
