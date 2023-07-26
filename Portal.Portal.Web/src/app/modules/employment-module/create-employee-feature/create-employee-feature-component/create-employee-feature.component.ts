import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { StepperActivateEvent } from '@progress/kendo-angular-layout';

import { EmployeeService } from 'src/app/shared/infrastructure/PortalHttpClient';
import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { CancelButtonClickEvent, EventBus, OkButtonClickEvent } from 'src/app/shared/infrastructure/CustomApi/event.bus';
import { CreateEmployeeViewStateModel } from 'src/app/modules/employment-module/create-employee-feature/create-employee-feature-component/createEmployeeViewStateModel';

@Component({
  selector: 'app-create-employee-feature',
  templateUrl: './create-employee-feature.component.html',
  styleUrls: ['./create-employee-feature.component.scss'],
})
export class CreateEmployeeFeatureComponent implements OnInit{
  viewState: CreateEmployeeViewStateModel = {
    employmentModuleResourceModel: {},
    errorResources: {},
    currentStep: 0,
    stepperColumnModel: {
      stepperCount: 3,
      stepperItemColumnCount: 1,
      stepperFooterColumnCount: 2
    },
    roundedModel: 'large',
    buttonSizeModel: 'large',
    createEmployeeFeatureStepsModel: [],
    stepperComponentModel: null
  }

  constructor(
    private globalResourceService: GlobalResourceService,
    private portalHttpClient: EmployeeService,
    private eventBus: EventBus,
  ) {}

  createEmployeeFeatureForm = new FormGroup({
    personalInformation: new FormGroup({
      fullName: new FormControl('', [Validators.required]),
      email: new FormControl('',[
        Validators.required,
        Validators.email
      ]),
      position: new FormControl('',[Validators.required]),
      jobType: new FormControl(undefined,[Validators.required]),
      phoneNumber: new FormControl('',[Validators.required]),
      personalId: new FormControl('',[Validators.required]),
      dateOfBirth: new FormControl(null, [Validators.required])
    }),
    employeeRole: new FormGroup({
      roleId: new FormControl(undefined),
    }),
  });

  ngOnInit(): void {
    this.loadResource();
  }

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.errorResources = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.createEmployeeFeatureStepsModel = [
      {
        label: this.viewState.employmentModuleResourceModel.personalInformation,
        isValid: this.isStepValid,
        validate: this.shouldValidate,
      },
      {
        label: this.viewState.employmentModuleResourceModel.role,
        isValid: this.isStepValid,
        validate: this.shouldValidate,
      },
      {
        label: this.viewState.employmentModuleResourceModel.schedule,
        isValid: this.isStepValid,
        validate: this.shouldValidate,
      },
    ]
  }

  get currentGroup(): FormGroup {
    return this.getGroupAt(this.viewState.currentStep);
  };

  shouldValidate = (index: number): boolean => {
    return this.getGroupAt(index)?.touched && this.viewState.currentStep >= index;
  };

  isCurrentStepViewState(stepIndex: number): boolean {
    return this.viewState.currentStep === stepIndex;
  };
  
  stepActiveViewState(ev: StepperActivateEvent): void {
    if (this.currentGroup?.invalid && ev.index !== this.viewState.createEmployeeFeatureStepsModel.length ) {
      ev.preventDefault();
      this.currentGroup.markAllAsTouched();
      this.viewState.stepperComponentModel?.validateSteps();
    }
  };

  nextButtonClickEvent(): void {
    if (this.currentGroup.valid && this.viewState.currentStep !== this.viewState.createEmployeeFeatureStepsModel.length) {
      this.viewState.currentStep += 1;
    } else {
      this.currentGroup.markAllAsTouched();
      this.viewState.stepperComponentModel?.validateSteps();
    }
    this.currentStepViewState(this.viewState.currentStep);
    console.log(this.createEmployeeFeatureForm.value)
  };

  previousButtonClickEvent(): void {
    this.viewState.currentStep -= 1;
    this.currentStepViewState(this.viewState.currentStep);
  };

  async createEmployeeButtonClickEvent(createEmployeeFeatureFormValue) {
    if (!this.createEmployeeFeatureForm.valid) {
      this.createEmployeeFeatureForm.markAllAsTouched();
      this.viewState.stepperComponentModel.validateSteps();
      return;
    } 
      
    try{
      const result = await firstValueFrom(this.portalHttpClient.apiEmployeeCreateemployeePost(createEmployeeFeatureFormValue));
      if (result.ok) {
        this.eventBus.send(new OkButtonClickEvent());
      }
    }catch(error){
      console.log(error);
    }
  };

  cancelEmployeeButtonClickEvent() {
    this.eventBus.send(new CancelButtonClickEvent())
  };

  private currentStepViewState(step: number): void {
    this.viewState.currentStep = step;
  };

  private isStepValid = (index: number): boolean => {
    return this.getGroupAt(index).valid || this.currentGroup?.untouched;
  };
  
  private getGroupAt(index: number): FormGroup {
    return this.createEmployeeFeatureForm.get(Object.keys(this.createEmployeeFeatureForm.controls)[index]) as FormGroup;
  };

}
