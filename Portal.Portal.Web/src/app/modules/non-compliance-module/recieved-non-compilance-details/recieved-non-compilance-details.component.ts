import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { RecievedNonCompilanceModuleDetailsModel } from './recievedNonCompilanceModuleDetailsModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { BASE_PATH, FileType, NonComplianceService, NonComplianceStatus, ResourceResponse } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-recieved-non-compilance-details',
  templateUrl: './recieved-non-compilance-details.component.html',
  styleUrls: ['./recieved-non-compilance-details.component.scss']
})
export class RecievedNonCompilanceDetailsComponent implements OnInit{
 
  layoutResource: ResourceResponse;

  viewState: RecievedNonCompilanceModuleDetailsModel = {
    activatedParam: '',
    basePath: '',
    selectedItemIndex: null,
    breadItems: [],
    nonCompilanceListUrl: [],
    loading: false,
    nonCompilanceRecievedDetailsViewModel: {},
    fileTypes: FileType,
    statusChangeDialogActionsViewState: false,
    statuses: NonComplianceStatus,
    rightOfStatusChangeHiddenField: false,
    isDisabledOngoingButton: false,
    redirectRequestViewState: false,
    openStatusChangeDialogViewState: false,
    loaderVisible: false,
    statusChangeSelectedFile: null,
    statusChangeSelectedFileExtensionOnUpload: '',
    statusChangeView: false,
    initialParticipants: [],
    recievers: [],
  }
  
  statusChangeForm = new FormGroup({
    status: new FormControl('', [Validators.required]),
    comment: new FormControl(''),
  })

  redirectForm = new FormGroup({
    reciever: new FormControl('', Validators.required),
    comment: new FormControl(''),
    rightOfStatusChange : new FormControl(false),
    file: new FormControl()
  })

  getFileClassByExtension = getFileClassByExtension;
  
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private nonCompilanceHttpClient: NonComplianceService,
    @Inject(BASE_PATH) basePath: string
  ) {
    this.viewState.activatedParam = this.activatedRoute.snapshot.params['number'];
    this.viewState.basePath = `${basePath}`;
  }

  ngOnInit(): void { 
    this.loadResource();
    this.nonCompilanceAllDetailsView(this.viewState.activatedParam);
  }

  breadItemsViewState(item: any): void {
    this.viewState.selectedItemIndex = this.viewState.breadItems.findIndex((i) => i.text === item.text);
    this.viewState.nonCompilanceListUrl = this.viewState.breadItems
      .slice(0, this.viewState.selectedItemIndex + 1)
      .map((i) => i.url);
    this.router.navigate(this.viewState.nonCompilanceListUrl);
  };

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
    this.viewState.breadItems = [
      {
        text: this.layoutResource.nonComplianceResources.nonCompliance,
        url: 'management/non-compliance'
      },
      {
        text: this.layoutResource.nonComplianceResources.receivedNonComplianceDetails
      },
    ];
  }

  redirectRequestViewState() {
    this.viewState.redirectRequestViewState = true;
    this.viewState.statusChangeDialogActionsViewState = false;
    this.employeeDataView(this.viewState.activatedParam);
  }

  async nonCompilanceAllDetailsView(number: string): Promise<void> {
    try {
      this.viewState.loading = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceReceivednoncompliancedetailsGet(number));

      if(response.ok) {
        this.viewState.loading = false;
        this.viewState.nonCompilanceRecievedDetailsViewModel = response.value;
        if(this.viewState.nonCompilanceRecievedDetailsViewModel.status ==  this.viewState.statuses['InProgress']) {
          this.viewState.statusChangeDialogActionsViewState = true;
        } else {
          this.viewState.statusChangeView =true;
        }
        if(!this.viewState.nonCompilanceRecievedDetailsViewModel.isStatusChanger) {
          this.viewState.isDisabledOngoingButton = true;
          this.viewState.rightOfStatusChangeHiddenField = true;
        }
      } else {
        this.viewState.loading = false;
      }
    }catch(error) {
      this.viewState.loading = false;
      console.log(error);
    }
  }

  openStatusChangeDialogButtonClickEvent() {
    this.viewState.openStatusChangeDialogViewState = true;
  }

  statusChangeFileSelectButtonClickEvent(event) {
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

  async employeeDataView(id): Promise<void> {
    try{
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceEmployeesGet('', id));

      if(response.ok) {
        this.viewState.initialParticipants = response.value.emails;
        this.viewState.recievers = response.value.emails;
      }
    }catch(error){
      console.log(error);
    }
  };


  recieverFilterEvent(input): void {
    this.viewState.recievers = this.viewState.initialParticipants.filter((string) => {
      return string.toLocaleLowerCase().includes(input.target.value.toLocaleLowerCase());
    });
  }

  async saveStatusChangeFormButtonClickEvent(statusChangeFormValue): Promise<void> {
    if (!this.statusChangeForm.valid || this.viewState.loaderVisible) {
      this.statusChangeForm.markAllAsTouched();
      return;
    } 

    try{
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceChangenoncompliancestatusPostForm(
        this.viewState.activatedParam, 
        statusChangeFormValue.status, 
        statusChangeFormValue.comment,
        this.viewState.statusChangeSelectedFile
      ));

      if(response.ok) {
        this.viewState.openStatusChangeDialogViewState = false;
        this.viewState.statusChangeDialogActionsViewState = false;
        this.nonCompilanceAllDetailsView(this.viewState.activatedParam);
        this.viewState.loaderVisible = false;
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
  }

  async saveRedirectFormButtonClickEvent(redirectFormValue): Promise<void> {
    
    if (!this.redirectForm.valid || this.viewState.loaderVisible) {
      this.redirectForm.markAllAsTouched();
      return;
    } 

    try {
      this.viewState.loaderVisible = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceRedirectnoncompliancePostForm(
        this.viewState.activatedParam,
        redirectFormValue.reciever,
        this.viewState.statusChangeSelectedFile,
        redirectFormValue.comment,
        redirectFormValue.rightOfStatusChange
      ));

      if(response.ok) {
        this.viewState.redirectRequestViewState = false;
        this.viewState.statusChangeDialogActionsViewState = false;
        this.viewState.statusChangeSelectedFile = null;
        this.nonCompilanceAllDetailsView(this.viewState.activatedParam);
        this.viewState.loaderVisible = false;
        this.redirectForm.reset();
      } else {
        this.viewState.loaderVisible = false;
      }
    }catch(error) {
      this.viewState.loaderVisible = false;
      console.log(error);
    }
  }

  closeStatusChangeDialogView() {
    this.viewState.openStatusChangeDialogViewState = false;
  }

  cancelStatusChangeFormButtonClickEvent() {
    this.viewState.openStatusChangeDialogViewState = false;
  }

  cancelRedirectFormButtonClickEvent() {
    this.viewState.redirectRequestViewState = false;
    this.viewState.statusChangeDialogActionsViewState = true;
    this.viewState.statusChangeSelectedFile = null;
    this.redirectForm.reset();
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }
  
}
