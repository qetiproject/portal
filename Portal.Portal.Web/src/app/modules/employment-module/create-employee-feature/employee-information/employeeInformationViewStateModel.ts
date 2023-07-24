import { InputRounded } from "@progress/kendo-angular-inputs";
import { EmploymentModuleResourceModel, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface EmployeeInformationViewStateModel {
    rounded: InputRounded;
    employmentModuleResourceModel: EmploymentModuleResourceModel; 
    phoneNumberMask: string;
    jobTypes: Array<{value: string, text: string}>;
    errorResourceList: ErrorResourceModel;
}