import { FormControl, FormGroup, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { Component, Inject, OnInit } from '@angular/core';

import { PageChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';

import { TimeOffRequestsModuleViewStateModel, TypeViewModel } from './timeOffRequestsModuleViewStateModel';

import { BASE_PATH, FileType, Recipient, TimeOffRequestService, TimeOffRequestStatus, TimeOffRequestType } from 'src/app/shared/infrastructure/PortalHttpClient';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { SelectEvent } from '@progress/kendo-angular-layout';
import { DatePipe } from '@angular/common';
import { getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-time-off-requests-module',
  templateUrl: './time-off-requests-module.component.html',
  styleUrls: ['./time-off-requests-module.component.scss']
})
export class TimeOffRequestsModuleComponent implements OnInit{
  
  viewState: TimeOffRequestsModuleViewStateModel = {
    basePath: '',
    timeOffRequestModuleResourceModel: {},
    errorResourceModel: {},
    initialAllTimeOffRequests: [],
    timeOffRequestAllViewModel: {
      timeOffRequests: [],
      total: 0
    },
    initialRecievedTimeOffRequests: [],
    timeOffRequestRecievedViewModel: {
      timeOffRequests: [],
      total: 0
    },
    initialSentTimeOffRequests: [],
    timeOffRequestSentViewModel: {
      timeOffRequests: [],
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
    searchTimeOffRequestValue: '',
    rounded: 'large',
    roundedSearch: 'full',
    buttonSize: 'large',
    isTimeOffRequestAllViewState: true,
    isTimeOffRequestRecievedViewState: false,
    isTimeOffRequestSentViewState: false,
    isActiveCreateDialogViewState: false,
    selectedFile: null,
    initialRecieverDataView: [],
    recieverDataView: [],
    tabSelectEventTitle: '',
    typesView: [],
    typeChangeValue: TimeOffRequestType.Custom,
    serverErrorsModel: [],
    fileTypes: FileType,
    statuses: TimeOffRequestStatus,
    filterRetrieveData: '',
    isLoadingOnGet: false,
    isLoadingOnPost: false,
    activeAllTab: false,
    activeSentTab: false,
    activeRecievedTab: false,
    openTimeOffRecievedRequestStatusChangeDialogViewState: false,
    statusChangeSelectedFile: null,
    loaderVisible: false,
    activateListItemNumber: '',
    isFromFieldViewState: false,
    isFromIncludingFieldViewState: false,
    isTitleFieldViewState: true,
    recipient: Recipient,
    hasHR: false,
    hasSupervisor: false,
    isRecieverFieldViewState: true,
    actionPermissions: {},
  }

  addTimeOffRequestForm = new FormGroup({
    type: new FormControl(),
    title: new FormControl(''),
    reciever: new FormControl(''),
    deadline: new FormControl(undefined),
    description: new FormControl(''),
    from: new FormControl(undefined),
    to: new FormControl(undefined),
    recipient: new FormControl(this.viewState.recipient.Other, Validators.required),
  })

  timeOffRequestRecievedHistoryForm = new FormGroup({
    status: new FormControl('', [Validators.required]),
    comment: new FormControl(''),
  })

  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private globalResourceService: GlobalResourceService,
    private timOffRequstHttpClient: TimeOffRequestService,
    private datePipe: DatePipe,
    @Inject(BASE_PATH) basePath: string
  ) {
    this.viewState.basePath = `${basePath}`;
    this.viewState.basePath = `${basePath}`;
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  }

  ngOnInit(): void {
    this.loadResource();
    this.selectActiveTab();
  };

  loadResource(): void {
    this.viewState.timeOffRequestModuleResourceModel = this.globalResourceService.resourceResponse.timeOffRequestResources;
    this.viewState.errorResourceModel = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.typesView = [
      { text:  this.viewState.timeOffRequestModuleResourceModel.sickLeave, value: TimeOffRequestType.SickLeave },
      { text:  this.viewState.timeOffRequestModuleResourceModel.dayOff, value: TimeOffRequestType.DayOff },
      { text:  this.viewState.timeOffRequestModuleResourceModel.parentalLeave, value: TimeOffRequestType.ParentalLeave },
      { text:  this.viewState.timeOffRequestModuleResourceModel.vacation, value: TimeOffRequestType.Vacation },
      { text:  this.viewState.timeOffRequestModuleResourceModel.businessTrip, value: TimeOffRequestType.BusinessTrip },
      { text:  this.viewState.timeOffRequestModuleResourceModel.unpaidVacation, value: TimeOffRequestType.UnpaidVacation },
      { text:  this.viewState.timeOffRequestModuleResourceModel.remoteDay, value: TimeOffRequestType.RemoteDay },
    ];
    this.isAllTimeOffRequestsPermission();
  }

  selectActiveTab(): void {
    let tabSelectEventTitle = localStorage.getItem('tabSelectEventTitle');

    switch(tabSelectEventTitle) {
      case this.viewState.timeOffRequestModuleResourceModel.all:
        this.viewState.activeAllTab = true;
        this.timeOffRequestAllView(this.viewState.searchTimeOffRequestValue);
        break;
      case this.viewState.timeOffRequestModuleResourceModel.sent:
        this.timeOffRequestSentView(this.viewState.searchTimeOffRequestValue);
        this.viewState.activeSentTab = true;
        this.viewState.activeAllTab = false;
        this.viewState.activeRecievedTab = false;
        break;
       case this.viewState.timeOffRequestModuleResourceModel.received:
        this.timeOffRequestRecievedView(this.viewState.searchTimeOffRequestValue);
        this.viewState.activeSentTab = false;
        this.viewState.activeAllTab = false;
        this.viewState.activeRecievedTab = true;
        break;
      default:
        this.isAllTimeOffRequestsPermission();
    }
  }

  isAllTimeOffRequestsPermission(): void {
    if(this.viewState.actionPermissions.allTimeOffRequests) {
      localStorage.setItem('tabSelectEventTitle', this.viewState.timeOffRequestModuleResourceModel.all);
      this.viewState.activeAllTab = true;
      this.viewState.activeSentTab = false;
      this.timeOffRequestAllView(this.viewState.searchTimeOffRequestValue);
    } else {
      localStorage.setItem('tabSelectEventTitle', this.viewState.timeOffRequestModuleResourceModel.sent);
      this.viewState.activeSentTab = true;
      this.viewState.activeAllTab = true;
      this.timeOffRequestSentView(this.viewState.searchTimeOffRequestValue);
    }
  }

  rowStyling = (context: RowClassArgs) => {
    if (context.index %2 !=0) {
      return { grey: true };
    } else {
      return { white: true };
    }
  };

  recipientChangeValue(value) {
    console.log(value, 'vvalue')
    if(value == this.viewState.recipient['HR'] || value == this.viewState.recipient['Supervisor']) {
      this.viewState.isRecieverFieldViewState = false;
    } else {
      this.viewState.isRecieverFieldViewState = true;
    }
  }

  createTimeOffRequestClickEvent(): void {
    this.viewState.isActiveCreateDialogViewState = true;
    this.viewState.isTitleFieldViewState = true;
    this.viewState.isFromFieldViewState = false;
    this.viewState.isFromIncludingFieldViewState = false;
    this.employeeDataView(this.viewState.filterRetrieveData);
    this.recipientViewState();
  };

  titleFieldViewState(typeChangeValue: string): void {
    switch(typeChangeValue) {
      case this.viewState.timeOffRequestModuleResourceModel.custom:
        this.viewState.isTitleFieldViewState = true;
        this.viewState.isFromFieldViewState = false;
        this.viewState.isFromIncludingFieldViewState = false;
        break;
    }
  }

  fromFieldViewState(typeChangeValue: string): void {
    switch(typeChangeValue) {
      case this.viewState.timeOffRequestModuleResourceModel.dayOff:
        this.viewState.isFromFieldViewState = true;
        this.viewState.isFromIncludingFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
      case this.viewState.timeOffRequestModuleResourceModel.sickLeave:
        this.viewState.isFromFieldViewState = true;
        this.viewState.isFromIncludingFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
      case this.viewState.timeOffRequestModuleResourceModel.parentalLeave:
        this.viewState.isFromFieldViewState = true;
        this.viewState.isFromIncludingFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
      case this.viewState.timeOffRequestModuleResourceModel.remoteDay:
        this.viewState.isFromFieldViewState = true;
        this.viewState.isFromIncludingFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
    }
  }

  fromIncludingFieldViewState(typeChangeValue: string): void {
    switch(typeChangeValue) {
      case this.viewState.timeOffRequestModuleResourceModel.vacation:
        this.viewState.isFromIncludingFieldViewState = true;
        this.viewState.isFromFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
      case this.viewState.timeOffRequestModuleResourceModel.businessTrip:
        this.viewState.isFromIncludingFieldViewState = true;
        this.viewState.isFromFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
       case this.viewState.timeOffRequestModuleResourceModel.unpaidVacation:
        this.viewState.isFromIncludingFieldViewState = true;
        this.viewState.isFromFieldViewState = false;
        this.viewState.isTitleFieldViewState = false;
        break;
    }
  }

  async timeOffRequestAllView(searchTimeOffValue: string): Promise<void> {
    this.viewState.isTimeOffRequestAllViewState = true;
    this.viewState.isTimeOffRequestRecievedViewState = false;
    this.viewState.isTimeOffRequestSentViewState = false;
    
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestAlltimeoffrequestsGet(searchTimeOffValue));
      if(response.ok) {
        this.viewState.initialAllTimeOffRequests = response.value.timeOffRequests;
        this.viewState.timeOffRequestAllViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async timeOffRequestSentView(searchTimeOff: string): Promise<void> {
    this.viewState.isTimeOffRequestAllViewState = false;
    this.viewState.isTimeOffRequestRecievedViewState = false;
    this.viewState.isTimeOffRequestSentViewState = true;
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestSenttimeoffrequestsGet(searchTimeOff));
      if(response.ok){
        this.viewState.initialSentTimeOffRequests = response.value.timeOffRequests;
        this.viewState.timeOffRequestSentViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async timeOffRequestRecievedView(searchTimeOff: string): Promise<void> {
    this.viewState.isTimeOffRequestAllViewState = false;
    this.viewState.isTimeOffRequestRecievedViewState = true;
    this.viewState.isTimeOffRequestSentViewState = false;
    try{
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestReceivedtimeoffrequestsGet(searchTimeOff));
      if(response.ok){
        this.viewState.initialRecievedTimeOffRequests = response.value.timeOffRequests;
        this.viewState.timeOffRequestRecievedViewModel = response.value;
        this.viewState.isLoadingOnGet = false;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error){
      this.viewState.isLoadingOnGet = false;
      console.log(error);
    }
  };

  async employeeDataView(data: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestEmployeesGet(data));

      if(response.ok) {
        this.viewState.initialRecieverDataView = response.value.emails;
        this.viewState.recieverDataView = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  };

  async recipientViewState(): Promise<void> {

    try{

      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestGetrecipientsGet());

      if(response.ok) {
        response.value.hasHR ? this.viewState.hasHR = true : this.viewState.hasHR = false;
        response.value.hasSupervisor ? this.viewState.hasSupervisor = true : this.viewState.hasSupervisor = false;
      }

    }catch(error){
      console.log(error);
    }
  }

  tabSelectButtonClickEvent(event: SelectEvent): void {
    this.viewState.tabSelectEventTitle = event.title;
    
    localStorage.setItem('tabSelectEventTitle', event.title);

    switch(event.title) {
      case this.viewState.timeOffRequestModuleResourceModel.all:
        this.timeOffRequestAllView(this.viewState.searchTimeOffRequestValue);
        break;
      case this.viewState.timeOffRequestModuleResourceModel.sent:
        this.timeOffRequestSentView(this.viewState.searchTimeOffRequestValue);
        break;
      case this.viewState.timeOffRequestModuleResourceModel.received:
        this.timeOffRequestRecievedView(this.viewState.searchTimeOffRequestValue);
        break;
      default:
        this.timeOffRequestAllView(this.viewState.searchTimeOffRequestValue);
    }
  }
  
  selectedFileClickEvent(event): void {
    console.log(event.files[0].rawFile)
    this.viewState.selectedFile = event.files[0].rawFile;
  };

  timeOffRequestPageChangeButtonClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  };

  timeOffRequestRecievedPageChangeButtonClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  };

  timeOffRequestSentPageChangeButtonClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  };

  timeOffAllFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value.toLowerCase();
    this.viewState.timeOffRequestAllViewModel.timeOffRequests = this.viewState.initialAllTimeOffRequests.filter((element): any => {
      if(element.title.toLowerCase().includes(inputValue) || element.sender.toLowerCase().includes(inputValue) || element.receiver.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  };

  timeOffFSentFilterInputEvent(input: Event): void{
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.timeOffRequestSentViewModel.timeOffRequests = this.viewState.initialSentTimeOffRequests.filter((element): any => {
      if(element.title?.toLowerCase().includes(inputValue) || element.receiver?.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  };

  timeOffRecievedFilterInputEvent(input: Event): void{
    const inputValue = (input.target as HTMLInputElement).value;
    this.viewState.timeOffRequestRecievedViewModel.timeOffRequests = this.viewState.initialRecievedTimeOffRequests.filter((element): any => {
      if(element.title?.toLowerCase().includes(inputValue) || element.sender?.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  };

  typeViewValueChangeEvent(event: TypeViewModel) {
    if(event.text) {
      this.viewState.typeChangeValue = event.text;
      this.viewState.serverErrorsModel = [];
      this.addTimeOffRequestForm.get('description').setValue(' ');
      this.addTimeOffRequestForm.get('title').setValue(' ');
    }
    this.titleFieldViewState(event.text);
    this.fromFieldViewState(event.text);
    this.fromIncludingFieldViewState(event.text);
  }

  recieverFilterEvent(input): void {
    this.viewState.recieverDataView = this.viewState.initialRecieverDataView.filter((string) => {
      return string.toLocaleLowerCase().includes(input.target.value.toLocaleLowerCase());
    });
  }
  
  async createTimeOffButtonClickEvent(addTimeOffRequestFormValue): Promise<void> {

    if (!this.addTimeOffRequestForm.valid || this.viewState.isLoadingOnPost) {
      this.addTimeOffRequestForm.markAllAsTouched();
      return;
    } 

    let requestType;
    if(addTimeOffRequestFormValue.type?.value != null) {
      requestType = addTimeOffRequestFormValue.type.value
    } else {
      requestType = TimeOffRequestType.Custom
    }

    try{
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestCreatetimeoffrequestPostForm(
        requestType,
        addTimeOffRequestFormValue.title, 
        addTimeOffRequestFormValue.recipient,
        addTimeOffRequestFormValue.reciever, 
        this.datePipe.transform(addTimeOffRequestFormValue.deadline, 'MM.dd.yyyy'), 
        this.datePipe.transform(addTimeOffRequestFormValue.from, 'MM.dd.yyyy'), 
        this.datePipe.transform(addTimeOffRequestFormValue.to, 'MM.dd.yyyy'), 
        this.viewState.selectedFile, 
        addTimeOffRequestFormValue.description
      ));
      if(response.ok){
        this.viewState.isLoadingOnPost = false;
        this.viewState.isActiveCreateDialogViewState = false;
        this.addTimeOffRequestForm.reset();
        
        let tabSelectEventTitle = localStorage.getItem('tabSelectEventTitle');
        switch(tabSelectEventTitle) {
          case this.viewState.timeOffRequestModuleResourceModel.all:
            this.timeOffRequestAllView(this.viewState.searchTimeOffRequestValue);
            break;
            case this.viewState.timeOffRequestModuleResourceModel.sent:
              this.timeOffRequestSentView(this.viewState.searchTimeOffRequestValue);
              break;
        }
        this.addTimeOffRequestForm.get('recipient').setValue(Recipient.Other);
        this.viewState.isRecieverFieldViewState = true;

        this.viewState.serverErrorsModel = [];

     } else {
        this.viewState.isLoadingOnPost = false;
        this.viewState.serverErrorsModel = response?.errors.messages;
      }
    }catch(error) {
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
      
  };

  cancelTimeOffRequestClickEvent() {
    this.viewState.isActiveCreateDialogViewState = false;
    this.addTimeOffRequestForm.reset();
    this.viewState.serverErrorsModel = [];
    this.addTimeOffRequestForm.get('description').setValue(' ');
    this.addTimeOffRequestForm.get('title').setValue(' ');
    this.addTimeOffRequestForm.get('recipient').setValue(Recipient.Other);
    this.viewState.isRecieverFieldViewState = true;
  }

  closeTimeOffRequestFormClickEvent() {
    this.viewState.isActiveCreateDialogViewState = false;
    this.addTimeOffRequestForm.reset();
    this.viewState.serverErrorsModel = [];
    this.addTimeOffRequestForm.get('description').setValue(' ');
    this.addTimeOffRequestForm.get('title').setValue(' ');
    this.addTimeOffRequestForm.get('recipient').setValue(Recipient.Other);
    this.viewState.isRecieverFieldViewState = true;
  };

  clearErrorsMessageboxClickEvent() {
    this.viewState.serverErrorsModel = [];
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  statusChangeFileSelectButtonClickEvent(event): void {
    this.viewState.statusChangeSelectedFile = event.files[0].rawFile;
  }

  async saveTimeOffRequestRecievedHistoryFormButtonClickEvent(timeOffRequestRecievedHistoryFormValue): Promise<void> {

    if (!this.timeOffRequestRecievedHistoryForm.valid || this.viewState.loaderVisible) {
      this.timeOffRequestRecievedHistoryForm.markAllAsTouched();
      return;
    } 

    try{
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestChangereceivedtimeoffrequeststatusPostForm(
        this.viewState.activateListItemNumber, 
        timeOffRequestRecievedHistoryFormValue.status, 
        timeOffRequestRecievedHistoryFormValue.comment,
        timeOffRequestRecievedHistoryFormValue.rawFile
      ));

      if(response.ok) {
        this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
        this.timeOffRequestRecievedView(this.viewState.searchTimeOffRequestValue);
        this.viewState.loaderVisible = false;
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
    
  }

  cancelTimeOffRequestRecievedHistoryFormButtonClickEvent(): void {
    this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
    this.timeOffRequestRecievedHistoryForm.reset();
  }
  
  statusChangeViewState(number: string, isStatusChanger: boolean): void {
    if(isStatusChanger){
      this.viewState.activateListItemNumber = number;
      this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = true;
    }
  }

  closeTimeOffRecievedRequestStatusChangeDialogView(): void {
    this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
  }

}
