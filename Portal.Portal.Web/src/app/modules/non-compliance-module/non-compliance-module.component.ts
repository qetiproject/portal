import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { firstValueFrom } from 'rxjs';

import { PageChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';
import { SelectEvent } from '@progress/kendo-angular-layout';

import { NonCompilanceViewStateModel } from './nonCompilanceViewStateModel';
import { getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { BASE_PATH, FileType, NonComplianceService, NonComplianceStatus, TimeOffRequestService } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-non-compliance-module',
  templateUrl: './non-compliance-module.component.html',
  styleUrls: ['./non-compliance-module.component.scss']
})
export class NonComplianceModuleComponent implements OnInit {
  
  viewState: NonCompilanceViewStateModel = {
    basePath: '',
    nonComplanceModuleResourceModel: {},
    errorResourceModel: {},
    rounded: 'large',
    roundedSearch: 'full',
    buttonSize: 'large',
    tabSelectEventTitle: '',
    isLoadingOnGet: false,
    isLoadingOnPost: false,
    initialNonCompilances: [],
    nonCompilanceAllViewModel: {
      nonCompliances: [],
      total: 0
    },
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
    searchNonCompilanceValue: '',
    isNonCompilanceAllViewState: false,
    statuses: NonComplianceStatus,
    fileTypes: FileType,
    isActiveCreateDialogViewState: false,
    violatorDataView: [],
    filterViolatorData: '',
    groupListViewData: [],
    groupType: '',
    serverErrorsModel: [],
    isViewTitle: true,
    selectedFile: null,
    isNonCompilanceSentViewState: false,
    initialSentNonCompilances: [],
    nonCompilanceSentViewModel: {
      nonCompliances: [],
      total: 0
    },
    initialReceivedNonCompilances: [],
    nonCompilanceRecievedViewModel: {
      nonCompliances: [],
      total: 0
    },
    isNonCompilanceRecievedViewState: false,
    initialMyNonCompilances: [],
    myNonCompilanceViewModel: {
      nonCompliances: [],
      total: 0
    },
    isMyNonCompilanceViewState: false,
    isLoadingRemoveGroup: false,
    activeAllTab: false,
    activeSentTab: false,
    activeRecievedTab: false,
    activeMyVolationsTab: false,
    activateListItemNumber: '',
    openStatusChangeDialogViewState: false,
    statusChangeSelectedFile: null,
    loaderVisible: false,
    actionPermissions: {},
  }

  createNonCompilanceForm = new FormGroup({
    group: new FormControl(undefined),
    title: new FormControl(''),
    violator: new FormControl('', Validators.required),
    fine: new FormControl(null),
    deadline: new FormControl(undefined),
    description: new FormControl('', Validators.required)
  })
  
  statusChangeForm = new FormGroup({
    status: new FormControl('', [Validators.required]),
    comment: new FormControl(''),
  })

  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private globalResourceService: GlobalResourceService,
    private nonComplianceHttpClient: NonComplianceService,
    private timeOffRequestHttpClient: TimeOffRequestService,
    private datePipe: DatePipe,
    @Inject(BASE_PATH) basePath: string
  ) {
    this.viewState.basePath = `${basePath}`;
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  }

  ngOnInit(): void {
    this.loadResource();
    this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
    this.selectActiveTab();
  }

  loadResource(): void {
    this.viewState.nonComplanceModuleResourceModel = this.globalResourceService.resourceResponse.nonComplianceResources;
    this.viewState.errorResourceModel = this.globalResourceService.resourceResponse.errorResources;
    this.isAllNonCompilancePermission();
  }

  rowStyling = (context: RowClassArgs) => {
    if (context.index %2 !=0) {
      return { grey: true };
    } else {
      return { white: true };
    }
  }

  selectActiveTab(): void {
    let tabSelectEventTitle = localStorage.getItem('tabSelectEventTitle');

    switch(tabSelectEventTitle) {
      case this.viewState.nonComplanceModuleResourceModel.all:
        this.viewState.activeAllTab = true;
        this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
        break;
      case this.viewState.nonComplanceModuleResourceModel.sent:
        this.nonCompilanceSentView(this.viewState.searchNonCompilanceValue);
        this.viewState.activeSentTab = true;
        this.viewState.activeAllTab = false;
        this.viewState.activeRecievedTab = false;
        break;
       case this.viewState.nonComplanceModuleResourceModel.received:
        this.nonCompilanceRecievedView(this.viewState.searchNonCompilanceValue);
        this.viewState.activeSentTab = false;
        this.viewState.activeAllTab = false;
        this.viewState.activeRecievedTab = true;
        break;
      case this.viewState.nonComplanceModuleResourceModel.myViolations:
        this.myNonCompilanceView(this.viewState.searchNonCompilanceValue);
        this.viewState.activeSentTab = false;
        this.viewState.activeAllTab = false;
        this.viewState.activeRecievedTab = false;
        this.viewState.activeMyVolationsTab = true;
        break;
      default:
        this.isAllNonCompilancePermission();
    }
  }
  
  isAllNonCompilancePermission(): void {
    if(this.viewState.actionPermissions.allNonCompliances) {
      localStorage.setItem('tabSelectEventTitle', this.viewState.nonComplanceModuleResourceModel.all);
      this.viewState.activeAllTab = true;
      this.viewState.activeSentTab = false;
      this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
    } else {
      localStorage.setItem('tabSelectEventTitle', this.viewState.nonComplanceModuleResourceModel.sent);
      this.viewState.activeSentTab = true;
      this.viewState.activeAllTab = true;
      this.nonCompilanceSentView(this.viewState.searchNonCompilanceValue);
    }
  }

  createNonCompilanceButtonClickEvent(): void {
    this.viewState.isActiveCreateDialogViewState = true;
    this.employeeDataView(this.viewState.filterViolatorData);
    this.groupListViewData();
  }

  async nonCompilanceAllView(searchNonCompilanceValue: string): Promise<void> {
    this.viewState.isNonCompilanceAllViewState = true;
    
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceAllnoncompliancesGet(searchNonCompilanceValue));
      if(response.ok) {
        this.viewState.initialNonCompilances = response.value.nonCompliances;
        this.viewState.nonCompilanceAllViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
      
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async nonCompilanceSentView(searchNonCompilanceValue: string): Promise<void> {
    this.viewState.isNonCompilanceSentViewState = true;
    
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceSentnoncompliancesGet(searchNonCompilanceValue));
      if(response.ok) {
        this.viewState.initialSentNonCompilances = response.value.nonCompliances;
        this.viewState.nonCompilanceSentViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;  
      }
      
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async nonCompilanceRecievedView(searchNonCompilanceValue: string): Promise<void> {
    this.viewState.isNonCompilanceRecievedViewState = true;
    
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceReceivednoncompliancesGet(searchNonCompilanceValue));
      if(response.ok) {
        this.viewState.initialReceivedNonCompilances = response.value.nonCompliances;
        this.viewState.nonCompilanceRecievedViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
      
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async myNonCompilanceView(searchNonCompilanceValue: string): Promise<void> {
    this.viewState.isMyNonCompilanceViewState = true;
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceMynoncompliancesGet(searchNonCompilanceValue));
      if(response.ok) {
        this.viewState.initialMyNonCompilances = response.value.nonCompliances;
        this.viewState.myNonCompilanceViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
      
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  }

  async employeeDataView(data: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.timeOffRequestHttpClient.apiTimeoffrequestEmployeesGet(data));

      if(response.ok) {
        this.viewState.violatorDataView = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  };

  tabSelectButtonClickEvent(event: SelectEvent): void {
    this.viewState.tabSelectEventTitle = event.title;

    localStorage.setItem('tabSelectEventTitle', event.title);
    switch(event.title) {
      case this.viewState.nonComplanceModuleResourceModel.all:
        this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
        break;
      case this.viewState.nonComplanceModuleResourceModel.sent:
        this.nonCompilanceSentView(this.viewState.searchNonCompilanceValue);
        break;
      case this.viewState.nonComplanceModuleResourceModel.received:
        this.nonCompilanceRecievedView(this.viewState.searchNonCompilanceValue);
        break;
      case this.viewState.nonComplanceModuleResourceModel.myViolations:
        this.myNonCompilanceView(this.viewState.searchNonCompilanceValue);
        break;
      default:
        this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
    }
  }

  nonCompilancePageChangeButtonClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  nonCompilanceSentPageChangeButtonClickEvent( {skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  nonCompilanceRecievedPageChangeButtonClickEvent( {skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  myNonCompilancePageChangeButtonClickEvent( {skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  nonCompilanceFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.nonCompilanceAllViewModel.nonCompliances = this.viewState.initialNonCompilances.filter((element): any => {
      if(element.group.toLowerCase().includes(inputValue) || element.violator.toLowerCase().includes(inputValue) || element.sender.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  }

  nonCompilanceSentFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.nonCompilanceSentViewModel.nonCompliances = this.viewState.initialSentNonCompilances.filter((element): any => {
      if(element.group.toLowerCase().includes(inputValue) || element.violator.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  }

  nonCompilanceRecievedFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.nonCompilanceRecievedViewModel.nonCompliances = this.viewState.initialReceivedNonCompilances.filter((element): any => {
      if(element.group.toLowerCase().includes(inputValue) || element.violator.toLowerCase().includes(inputValue) || element.sender.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  }

  myNonCompilanceFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.myNonCompilanceViewModel.nonCompliances = this.viewState.initialMyNonCompilances.filter((element): any => {
      if(element.group.toLowerCase().includes(inputValue) || element.sender.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  }

  groupValueChangeEvent(value): void {
    this.viewState.serverErrorsModel = [];
    this.viewState.groupType = value.group;
    if(value.group != 'Custom') {
      this.viewState.isViewTitle = false;
    } else {
      this.viewState.isViewTitle = true;
    }
  }

  violatorFilterEvent(input): void {
    const inputValue = (input.target as HTMLInputElement).value;
    this.employeeDataView(inputValue);
  }

  fileSelectButtonClickEvent(event): void {
    this.viewState.selectedFile = event.files[0].rawFile;
  }

  async groupListViewData(): Promise<void> {

    try {
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceGroupslistGet());

      if(response.ok) {
        this.viewState.groupListViewData = [{group: 'Custom', id: null}, ...response.value.groups];
        this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
      }
    }catch(error) {
      console.log(error);
    }
  }

  async createNonCompilanceFormButtonClickEvent(createNonCompilanceFormValue): Promise<void> {

    if (!this.createNonCompilanceForm.valid || this.viewState.isLoadingOnPost) {
      this.createNonCompilanceForm.markAllAsTouched();
      return;
    } 
    

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceCreatenoncompliancePostForm(
        createNonCompilanceFormValue.title || this.viewState.groupType,
        createNonCompilanceFormValue.violator,
        this.datePipe.transform(createNonCompilanceFormValue.deadline, 'MM.dd.yyyy'),
        createNonCompilanceFormValue.fine,
        this.viewState.selectedFile,
        createNonCompilanceFormValue.description
      ));

      if(response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.isActiveCreateDialogViewState = false;
        this.createNonCompilanceForm.reset();
        
        switch(this.viewState.tabSelectEventTitle) {
          case this.viewState.nonComplanceModuleResourceModel.all:
            this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
            break;
          case this.viewState.nonComplanceModuleResourceModel.sent:
            this.nonCompilanceSentView(this.viewState.searchNonCompilanceValue);
            break;
        }

        this.viewState.serverErrorsModel = [];
      } else {
        this.viewState.isLoadingOnPost = false;
        this.viewState.serverErrorsModel = response.errors.messages;
      }
    }catch(error) {
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
  }

  closeCreateNonCompilanceFormClickEvent():void {
    this.viewState.isActiveCreateDialogViewState = false;
    this.viewState.serverErrorsModel = [];
    this.createNonCompilanceForm.reset();
  }

  cancelCreateNonCompilanceClickEvent(): void {
    this.viewState.isActiveCreateDialogViewState = false;
    this.viewState.serverErrorsModel  = [];
    this.createNonCompilanceForm.reset();
  }
  
  clearErrorsMessageboxClickEvent(): void {
    this.viewState.serverErrorsModel = [];
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  closeStatusChangeDialogView() {
    this.viewState.openStatusChangeDialogViewState = false;
  }

  statusChangeFileSelectButtonClickEvent(event) {
    this.viewState.statusChangeSelectedFile = event.files[0].rawFile;
  }

  async saveStatusChangeFormButtonClickEvent(statusChangeFormValue): Promise<void> {
    if (!this.statusChangeForm.valid || this.viewState.loaderVisible) {
      this.statusChangeForm.markAllAsTouched();
      return;
    } 

    try{
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.nonComplianceHttpClient.apiNoncomplianceChangenoncompliancestatusPostForm(
        this.viewState.activateListItemNumber, 
        statusChangeFormValue.status, 
        statusChangeFormValue.comment,
        this.viewState.statusChangeSelectedFile
      ));

      if(response.ok) {
        this.viewState.openStatusChangeDialogViewState = false;
        if(this.viewState.activeAllTab) {
          this.nonCompilanceAllView(this.viewState.searchNonCompilanceValue);
        }
        if(this.viewState.activeRecievedTab) {
          this.nonCompilanceRecievedView(this.viewState.searchNonCompilanceValue);
        }
        this.viewState.loaderVisible = false;
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
  }

  cancelStatusChangeFormButtonClickEvent(): void {
    this.viewState.openStatusChangeDialogViewState = false;
  }

  statusChangeViewState(number: string, isStatusChanger: boolean): void {
    if(isStatusChanger) {
      this.viewState.activateListItemNumber = number;
      this.viewState.openStatusChangeDialogViewState = true;
    }
  }

}
