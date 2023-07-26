import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { SentNonCompilanceModuleDetailsModel } from './sentNonCompilanceModuleDetailsModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { BASE_PATH, FileType, NonComplianceService, NonComplianceStatus, ResourceResponse } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-sent-non-compilance-details',
  templateUrl: './sent-non-compilance-details.component.html',
  styleUrls: ['./sent-non-compilance-details.component.scss']
})
export class SentNonCompilanceDetailsComponent {
  layoutResource: ResourceResponse;

  viewState: SentNonCompilanceModuleDetailsModel = {
    activatedParam: '',
    basePath: '',
    selectedItemIndex: null,
    breadItems: [],
    nonCompilanceListUrl: [],
    isLoadingOnGet: false,
    nonCompilanceSentDetailsViewModel: {},
    fileTypes: FileType,
    statuses: NonComplianceStatus,
    statusChangeView: false,
  }

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
    this.nonCompilanceSentDetailsView(this.viewState.activatedParam);
  }

  loadResource(): void {
    this.layoutResource = this.globalResourceService.resourceResponse;
    this.viewState.breadItems = [
      {
        text: this.layoutResource.nonComplianceResources.nonCompliance,
        url: 'management/non-compliance'
      },
      {
        text: this.layoutResource.nonComplianceResources.sentNonComplianceDetails
      },
    ];
  }

  breadItemsViewState(item: any): void {
    this.viewState.selectedItemIndex = this.viewState.breadItems.findIndex((i) => i.text === item.text);
    this.viewState.nonCompilanceListUrl = this.viewState.breadItems
      .slice(0, this.viewState.selectedItemIndex + 1)
      .map((i) => i.url);
    this.router.navigate(this.viewState.nonCompilanceListUrl);
  };

  async nonCompilanceSentDetailsView(number: string): Promise<void> {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceSentnoncompliancedetailsGet(number));

      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.nonCompilanceSentDetailsViewModel = response.value;
        if(this.viewState.nonCompilanceSentDetailsViewModel.status !==  this.viewState.statuses['InProgress']) {
          this.viewState.statusChangeView =true;
        }
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    }catch(error) {
      this.viewState.isLoadingOnGet = false;
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
