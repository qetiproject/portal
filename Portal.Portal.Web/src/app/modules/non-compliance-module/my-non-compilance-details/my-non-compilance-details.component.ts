import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { MyNonCompilanceDetailsModel } from './myNonCompilanceDetailsModel';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { assignFileTypeForUpload, getFileClassByExtension } from 'src/app/shared/infrastructure/Utils/utils';
import { BASE_PATH, FileType, NonComplianceService, NonComplianceStatus, ResourceResponse } from 'src/app/shared/infrastructure/PortalHttpClient';

@Component({
  selector: 'app-my-non-compilance-details',
  templateUrl: './my-non-compilance-details.component.html',
  styleUrls: ['./my-non-compilance-details.component.scss']
})
export class MyNonCompilanceDetailsComponent implements OnInit{
  
  layoutResource: ResourceResponse;

  viewState: MyNonCompilanceDetailsModel = {
    activatedParam: '',
    basePath: '',
    selectedItemIndex: null,
    breadItems: [],
    nonCompilanceListUrl: [],
    myNonCompilanceDetailsViewModel: {},
    fileTypes: FileType,
    addCommentFormViewState: false,
    uploadFile: null,
    uploadFileExtensionOnUpload: '',
    isMyComment: false,
    statusChangeView: false,
    statuses: NonComplianceStatus,
    isLoadingOnGet: false,
    isLoadingOnPost: false
  }

  commentForm = new FormGroup({
    comment: new FormControl('', Validators.required),
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
    this.myNonCompilanceDetailsView(this.viewState.activatedParam);
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
        text: this.layoutResource.nonComplianceResources.myNonComplianceDetails
      },
    ];
  }

  async myNonCompilanceDetailsView(number: string): Promise<void> {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceMynoncompliancedetailsGet(number));

      if(response.ok) {
        this.viewState.isLoadingOnGet = false;
        if(response.value.myComment|| response.value.myFile.type) {
          this.viewState.isMyComment = true;
        } else {
          this.viewState.isMyComment = false;
        }
        this.viewState.myNonCompilanceDetailsViewModel = response.value;
        if(this.viewState.myNonCompilanceDetailsViewModel.status !=  this.viewState.statuses['InProgress']) {
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

  addCommentFormVewState() {
    this.viewState.addCommentFormViewState = true;
  }

  uploadFileSelectButtonClickEvent(event): void {
    this.viewState.uploadFile = event.target.files[0];
    if(event.target.files[0].name) {
      this.viewState.uploadFileExtensionOnUpload = assignFileTypeForUpload(event);
    } else {
      this.viewState.uploadFileExtensionOnUpload = '';
    }
    event.target.value = '';
  }

  deleteUploadedFileButtonClickEvent(): void {
    this.viewState.uploadFile = null;
  }

  openFileButtonClickEvent(data): void {
    if(data.path) {
      const fileUrl = `${this.viewState.basePath}/api/files/download?Name=${data.name}&Path=${data.path}`;
      window.open(fileUrl, '_blank');
    }
  }

  async saveCommentFormButtonClickEvent(commentFormValue): Promise<void> {

    if (!this.commentForm.valid || this.viewState.isLoadingOnPost) {
      this.commentForm.markAllAsTouched();
      return;
    } 

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.nonCompilanceHttpClient.apiNoncomplianceAddnoncompliancecommentPostForm(
        this.viewState.activatedParam,
        commentFormValue.comment,
        this.viewState.uploadFile
      ))

      if(response.ok) {
        this.viewState.isLoadingOnPost = false;
        this.viewState.uploadFile = null;
        this.commentForm.reset();
        this.viewState.addCommentFormViewState = false;
        this.viewState.isMyComment = true;
        this.myNonCompilanceDetailsView(this.viewState.activatedParam);
      } else {
        this.viewState.isLoadingOnPost = false;
      }
      
    }catch(error) {
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }

  }

  cancelCommentFormButtonClickEvent(): void {
    this.viewState.addCommentFormViewState = false;
    this.viewState.uploadFile = null;
    this.commentForm.reset();
  }

}
