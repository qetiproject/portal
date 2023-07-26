import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { TimeOffRequestRecievedHistoryViewModel } from './timeOffRequestRecievedHistoryViewModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { BASE_PATH, FileType, ResourceResponse, TimeOffRequestService, TimeOffRequestStatus, TimeOffRequestType } from 'src/app/shared/infrastructure/PortalHttpClient';


@Component({
  selector: 'app-time-off-request-recieved-history',
  templateUrl: './time-off-request-recieved-history.component.html',
  styleUrls: ['./time-off-request-recieved-history.component.scss']
})
export class TimeOffRequestRecievedHistoryComponent implements OnInit{
  
  layoutResource: ResourceResponse;

  viewState: TimeOffRequestRecievedHistoryViewModel = {
    activatedParam: '',
    basePath: '',
    selectedItemIndex: null,
    breadItems: [],
    timeOffRequestListUrl: [],
    timeOffRequestRecievedHistoryViewModel: {},
    timeOffRequestType: TimeOffRequestType,
    fileIcon: false,
    openTimeOffRecievedRequestStatusChangeDialogViewState: false,
    statuses: TimeOffRequestStatus,
    statusChangeSelectedFile: null,
    statusChangeSelectedFileExtensionOnUpload: '',
    statusChangeDialogActionsViewState: false,
    timeOffRecievedRedirectRequestViewState: false,
    initialParticipants: [],
    participants: [],
    isDisabledOngoingButton: false,
    redirectHistoryStatusChangeView: false,
    rightOfConfirmationHiddenField: false,
    fileTypes: FileType,
    loading: false,
    loaderVisible: false,
    filterRetrieveData: '',
  }

  timeOffRequestRecievedHistoryForm = new FormGroup({
    status: new FormControl('', [Validators.required]),
    comment: new FormControl(''),
  })

  timeOffRequestRecievedRedirectForm = new FormGroup({
    participant: new FormControl('', Validators.required),
    comment: new FormControl(''),
    rightOfConfirmation : new FormControl(false),
  })

  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private timOffRequstHttpClient: TimeOffRequestService,
    private globalResourceService: GlobalResourceService,
    @Inject(BASE_PATH) basePath: string
  ) {
    this.viewState.activatedParam = this.activatedRoute.snapshot.params['number'];
    this.viewState.basePath = `${basePath}`;
  }

  ngOnInit(): void {
    this.timeOffRequestRecievedHistoryView(this.viewState.activatedParam);
    this.employeeDataView(this.viewState.filterRetrieveData);
    this.loadResource();
   }
  
  breadItemsViewState(item: any): void {
    this.viewState.selectedItemIndex = this.viewState.breadItems.findIndex((i) => i.text === item.text);
    this.viewState.timeOffRequestListUrl = this.viewState.breadItems
      .slice(0, this.viewState.selectedItemIndex + 1)
      .map((i) => i.url);
    this.router.navigate(this.viewState.timeOffRequestListUrl);
  };

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
    this.viewState.breadItems = [
      {
        text: this.layoutResource.timeOffRequestResources.timeOffRequests,
        url: 'management/time-off-requests'
      },
      {
        text: this.layoutResource.timeOffRequestResources.receivedRequestHistory
      },
    ];
  }

  async timeOffRequestRecievedHistoryView(number: string): Promise<void> {
    this.viewState.loading = true;
    try {
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestReceivedtimeoffrequesthistoryGet(number));

      if(response.ok) {
        this.viewState.loading = false;
        this.viewState.timeOffRequestRecievedHistoryViewModel = response.value;
        if(this.viewState.timeOffRequestRecievedHistoryViewModel.status ==  this.viewState.statuses['InProgress']) {
          this.viewState.statusChangeDialogActionsViewState = true;
        } else {
          this.viewState.redirectHistoryStatusChangeView =true;
        }
        if(!this.viewState.timeOffRequestRecievedHistoryViewModel.isStatusChanger) {
          this.viewState.isDisabledOngoingButton = true;
          this.viewState.rightOfConfirmationHiddenField = true;
        }
      } else {
        this.viewState.loading = false;
      }
    }catch(error) {
      this.viewState.loading = false;
      console.log(error);
    }
  }

  async employeeDataView(data: string): Promise<void> {
    try{
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestEmployeesGet(data));

      if(response.ok) {
        this.viewState.initialParticipants = response.value.emails;
        this.viewState.participants = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  };


  participantFilterEvent(input): void {
    this.viewState.participants = this.viewState.initialParticipants.filter((string) => {
      return string.toLocaleLowerCase().includes(input.target.value.toLocaleLowerCase());
    });
  }
  
  openTimeOffRecievedRequestStatusChangeDialogButtonClickEvent(): void {
    this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = true;
  }

  statusChangeFileSelectButtonClickEvent(event): void {
    this.viewState.statusChangeSelectedFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.statusChangeSelectedFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.statusChangeSelectedFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  deleteStatusChangeFileButtonClickEvent() {
    this.viewState.statusChangeSelectedFile = null;
  }

  async saveTimeOffRequestRecievedHistoryFormButtonClickEvent(timeOffRequestRecievedHistoryFormValue): Promise<void> {

    if (!this.timeOffRequestRecievedHistoryForm.valid || this.viewState.loaderVisible) {
      this.timeOffRequestRecievedHistoryForm.markAllAsTouched();
      return;
    } 

    try{
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestChangereceivedtimeoffrequeststatusPostForm(
        this.viewState.activatedParam, 
        timeOffRequestRecievedHistoryFormValue.status, 
        timeOffRequestRecievedHistoryFormValue.comment,
        timeOffRequestRecievedHistoryFormValue.rawFile
      ));

      if(response.ok) {
        this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
        this.viewState.statusChangeDialogActionsViewState = false;
        this.timeOffRequestRecievedHistoryView(this.viewState.activatedParam);
        this.viewState.loaderVisible = false;
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
    
  }

  async saveTimeOffRequestRedirectFormButtonClickEvent(timeOffRequestRecievedRedirectFormValue): Promise<void> {
    
    if (!this.timeOffRequestRecievedRedirectForm.valid || this.viewState.loaderVisible) {
      this.timeOffRequestRecievedRedirectForm.markAllAsTouched();
      return;
    } 

    try {
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestRedirecttimeoffrequestPostForm(
        this.viewState.activatedParam,
        timeOffRequestRecievedRedirectFormValue.participant,
        this.viewState.statusChangeSelectedFile,
        timeOffRequestRecievedRedirectFormValue.comment,
        timeOffRequestRecievedRedirectFormValue.rightOfConfirmation
      ));

      if(response.ok) {
        this.viewState.timeOffRecievedRedirectRequestViewState = false;
        this.viewState.statusChangeDialogActionsViewState = false;
        this.timeOffRequestRecievedHistoryView(this.viewState.activatedParam);
        this.viewState.loaderVisible = false;
        this.viewState.statusChangeSelectedFile = null;
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
  }

  timeOffRecievedRedirectRequestViewState(): void {
    this.viewState.timeOffRecievedRedirectRequestViewState = true;
    this.viewState.statusChangeDialogActionsViewState = false;
  }

  closeTimeOffRecievedRequestStatusChangeDialogView(): void {
    this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
  }

  cancelTimeOffRequestRecievedHistoryFormButtonClickEvent(): void {
    this.viewState.openTimeOffRecievedRequestStatusChangeDialogViewState = false;
    this.timeOffRequestRecievedHistoryForm.reset();
  }

  cancelTimeOffRequestRedirectFormButtonClickEvent(): void {
    this.viewState.timeOffRecievedRedirectRequestViewState = false;
    this.viewState.statusChangeDialogActionsViewState = true;
    this.viewState.statusChangeSelectedFile = null;
    this.timeOffRequestRecievedRedirectForm.reset();
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }
  
}
  