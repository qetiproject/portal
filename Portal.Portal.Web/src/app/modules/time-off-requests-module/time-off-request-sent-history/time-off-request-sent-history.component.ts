import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { TimeOffRequestSentHistoryViewModel } from './timeOffRequestSentHistoryViewModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { BASE_PATH, FileType, ResourceResponse, TimeOffRequestService, TimeOffRequestStatus, TimeOffRequestType } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-time-off-request-sent-history',
  templateUrl: './time-off-request-sent-history.component.html',
  styleUrls: ['./time-off-request-sent-history.component.scss']
})
export class TimeOffRequestSentHistoryComponent implements OnInit{

  layoutResource: ResourceResponse;
  
  viewState: TimeOffRequestSentHistoryViewModel = {
    activatedParam: '',
    basePath: '',
    selectedItemIndex: null,
    breadItems: [],
    timeOffRequestListUrl: [],
    timeOffRequestSentHistoryViewModel: {},
    timeOffRequestType: TimeOffRequestType,
    fileIcon: false,
    redirectHistoryStatusChangeView: false,
    fileTypes: FileType,
    loading: false,
    statuses: TimeOffRequestStatus,
  }
  
  getFileClassByExtension = getFileClassByExtension;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private timOffRequstHttpClient: TimeOffRequestService,
    private globalResourceService: GlobalResourceService,
    @Inject(BASE_PATH) basePath: string
    ) {
    this.viewState.activatedParam = this.activatedRoute.snapshot.params['number'];
    this.viewState.basePath = `${basePath}`;
  }

  ngOnInit(): void {
    this.timeOffRequestSentHistoryView(this.viewState.activatedParam);
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
        text: this.layoutResource.timeOffRequestResources.sentRequestDetails
      },
    ];
  }
  
  async timeOffRequestSentHistoryView(number: string): Promise<void> {
    try {
      this.viewState.loading = true;
      const response = await firstValueFrom(this.timOffRequstHttpClient.apiTimeoffrequestSenttimeoffrequestdetailsGet(number));

      if(response.ok) {
        this.viewState.loading = false;
        this.viewState.timeOffRequestSentHistoryViewModel = response.value;
        if(this.viewState.timeOffRequestSentHistoryViewModel.status != this.viewState.statuses['InProgress']) {
          this.viewState.redirectHistoryStatusChangeView = true;
        }
      } else {
          this.viewState.loading = false;
        }
      }catch(error) {
      this.viewState.loading = false;
      console.log(error);
    }
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }
}
