import { ButtonSize } from "@progress/kendo-angular-buttons";
import { InputRounded } from "@progress/kendo-angular-inputs";
import { StepperComponent } from "@progress/kendo-angular-layout";

import { StepperColumnModel } from "src/app/shared/infrastructure/Model/stepperColumnModel";
import { EmploymentModuleResourceModel, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

interface CreateEmployeeFeatureStepsModel {
  label: string;
  isValid: (index: number) => void;
  validate: (index: number) => void;
}

export interface CreateEmployeeViewStateModel {
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    errorResources: ErrorResourceModel;
    currentStep: number;
    stepperColumnModel: StepperColumnModel;
    roundedModel: InputRounded;
    buttonSizeModel: ButtonSize;
    createEmployeeFeatureStepsModel: Array<CreateEmployeeFeatureStepsModel>;
    stepperComponentModel: StepperComponent;
  }
