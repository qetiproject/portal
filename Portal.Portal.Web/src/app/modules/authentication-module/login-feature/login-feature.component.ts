import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Component, OnInit, ViewChild } from "@angular/core";
import { firstValueFrom } from "rxjs";

import { TextBoxComponent } from "@progress/kendo-angular-inputs";

import { LoginFeatureViewStateModel } from "./loginFeatureViewStateModel";
import { AuthService } from "src/app/shared/infrastructure/CustomApi/auth.service";
import { LogInService, LoginRequest } from "src/app/shared/infrastructure/PortalHttpClient";
import { GlobalResourceService } from "src/app/shared/infrastructure/CustomApi/global-resource.service";

@Component({
  selector: 'app-login-feature',
  templateUrl: './login-feature.component.html',
  styleUrls: ['./login-feature.component.scss']
})
export class LoginFeatureComponent implements OnInit {
  @ViewChild("password") public passwordTextbox: TextBoxComponent;
  
  viewState: LoginFeatureViewStateModel = {
    authorizationModuleResourceModel: {},
    errorResourceListModel: {},
    roundedModel: 'large',
    serverErrorsModel: {
      messages: []
    },
    inputPasswordType: 'password',
    inputTextType: 'text',
  }

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    // checkbox: new FormControl(false)
  });

  constructor(
    private globalResourceService: GlobalResourceService,
    private loginService: LogInService,
    private authService: AuthService,
  ) {
  }

  ngOnInit() {
    this.loadResource();
  };

  loadResource(): void {
    this.viewState.authorizationModuleResourceModel = this.globalResourceService.resourceResponse.authorizationModuleResources;
    this.viewState.errorResourceListModel = this.globalResourceService.resourceResponse.errorResources;
  }

  ngAfterViewInit(): void {
    this.passwordTextbox.input.nativeElement.type = this.viewState.inputPasswordType;
  };

  togglePasswordEyeVisibilityViewState(): void {
    const inputEl = this.passwordTextbox.input.nativeElement;
    inputEl.type = inputEl.type === this.viewState.inputPasswordType ? this.viewState.inputTextType : this.viewState.inputPasswordType;
  };

  async loginButtonClickEvent(loginFormValue: LoginRequest): Promise<void>{
    this.passwordTextbox.input.nativeElement.type = this.viewState.inputPasswordType;

    if (!this.loginForm.valid) {
      this.loginForm.markAllAsTouched();
      return;
    } 
   
    try{
      const response = await firstValueFrom(this.loginService.apiLoginLoginPost(loginFormValue));
      if(response.ok) {
        this.authService.login(response.value);
      }else {
        this.viewState.serverErrorsModel = response.errors;
      }
    }catch(error) {
      console.log(error);
    }

  };

  clearErrorsMessageboxClickEvent() {
    this.viewState.serverErrorsModel.messages = [];
  }

}
