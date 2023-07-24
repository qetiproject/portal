import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TextBoxComponent } from '@progress/kendo-angular-inputs';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';

import { ResetPassowrdViewStateModel } from './resetPassowrdViewStateModel';

import { LogInService, ResetPasswordRequest } from 'src/app/shared/infrastructure/PortalHttpClient';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';

import { mustMatch } from 'src/app/shared/infrastructure/Utils/validators.fn';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit{
  @ViewChild("password") public passwordTextbox: TextBoxComponent;
  @ViewChild("confirmPassword") public confirmPasswordTextbox: TextBoxComponent;
  
  viewState: ResetPassowrdViewStateModel = {
    authorizationModuleResourceModel: {},
    errorResourceListModel: {},
    serverErrorsModel: {
      messages: []
    },
    isLoadingOnPost: false,
    activatedParam: '',
    inputPasswordType: 'password',
    inputTextType: 'text',
    inputconfirmPasswordType: 'password',
    inputConfirmTextType: 'text',
    isPasswordReset: false,
    resetToken: '',
    resetEmail: '',
  }

  resetPasswordForm = new FormGroup({
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: mustMatch('password', 'confirmPassword')
  });

  constructor(
    private globalResourceService: GlobalResourceService,
    private loginHttpClient: LogInService,
    private activatedRoute: ActivatedRoute,
  ) {
    this.viewState.resetToken =  this.activatedRoute.snapshot.params['token'];
    this.viewState.resetEmail =  this.activatedRoute.snapshot.params['email'];
  }

  ngOnInit(): void {
    this.loadResource();
  }

  ngAfterViewInit(): void {
    if(this.passwordTextbox) {
      this.passwordTextbox.input.nativeElement.type = this.viewState.inputPasswordType;
    }
    if(this.confirmPasswordTextbox) {
      this.confirmPasswordTextbox.input.nativeElement.type = this.viewState.inputconfirmPasswordType;
    }
  };

  loadResource(): void {
    this.viewState.authorizationModuleResourceModel = this.globalResourceService.resourceResponse.authorizationModuleResources;
    this.viewState.errorResourceListModel = this.globalResourceService.resourceResponse.errorResources;
  }

  togglePasswordEyeVisibilityViewState(): void {
    const inputEl = this.passwordTextbox.input.nativeElement;
    inputEl.type = inputEl.type === this.viewState.inputPasswordType ? this.viewState.inputTextType : this.viewState.inputPasswordType;
  };

  toggleConfirmPasswordEyeVisibilityViewState(): void {
    const inputEl = this.confirmPasswordTextbox.input.nativeElement;
    inputEl.type = inputEl.type === this.viewState.inputconfirmPasswordType ? this.viewState.inputConfirmTextType : this.viewState.inputconfirmPasswordType;
  }

  async resetButtonClickEvent(resetPasswordFormValue): Promise<void> {

    if (!this.resetPasswordForm.valid) {
      this.resetPasswordForm.markAllAsTouched();
      return;
    } 
        
    let resetPasswordModel: ResetPasswordRequest  = {
      password: resetPasswordFormValue.password,
      confirmPassword: resetPasswordFormValue.confirmPassword,
      email:  this.viewState.resetEmail,
      token: this.viewState.resetToken,
    }

    try {
      this.viewState.isLoadingOnPost = true;
      const response = await firstValueFrom(this.loginHttpClient.apiLoginResetpasswordPost(resetPasswordModel));

      if(response.ok) {
        this.viewState.isPasswordReset = true;
        this.viewState.isLoadingOnPost = false;
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
