import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { ForgotPassowrdViewStateModel } from './forgotPasswordViewStateModel';
import { ForgotPasswordRequest, LogInService } from 'src/app/shared/infrastructure/PortalHttpClient';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit{

  viewState: ForgotPassowrdViewStateModel = {
    authorizationModuleResourceModel: {},
    errorResourceListModel: {},
    serverErrorsModel: {
      messages: []
    },
    isForgotPasswordButton: false,
    userEmail: '',
    isLoadingOnPost: false,
  }

  forgotPassowrdForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email])
  });

  constructor(
    private globalResourceService: GlobalResourceService,
    private loginHttpClient: LogInService
  ) {}

  ngOnInit(): void {
    this.loadResource();
  }

  loadResource(): void {
    this.viewState.authorizationModuleResourceModel = this.globalResourceService.resourceResponse.authorizationModuleResources;
    this.viewState.errorResourceListModel = this.globalResourceService.resourceResponse.errorResources;
  }

  async resetButtonClickEvent(forgotPasswordFormValue): Promise<void> {

    if (!this.forgotPassowrdForm.valid) {
      this.forgotPassowrdForm.markAllAsTouched();
      return;
    } 
    
    let forgotPasswordRequestModel: ForgotPasswordRequest = {
      email: forgotPasswordFormValue.email
    }
    
    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.loginHttpClient.apiLoginForgotpasswordPost(forgotPasswordRequestModel));

      if(response.ok) {
        this.viewState.isForgotPasswordButton = true;
        this.viewState.isLoadingOnPost = false;
        this.viewState.userEmail = forgotPasswordFormValue.email;
      } else {
        this.viewState.isLoadingOnPost = false;
        this.viewState.serverErrorsModel = response.errors;
      }
    }catch(error) {
      this.viewState.isLoadingOnPost = false;
      console.log(error);
    }
  }

  clearErrorsMessageboxClickEvent(): void {
    this.viewState.serverErrorsModel.messages = [];
  }

}
