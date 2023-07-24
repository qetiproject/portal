import { ActionPermissions, EditEmployeePayrollBasicRequest, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";
import { BanksResponse } from "src/app/shared/infrastructure/PortalHttpClient/model/banksResponse";
import { EmployeePayrollBasicResponse } from "src/app/shared/infrastructure/PortalHttpClient/model/employeePayrollBasicResponse";
import { EmploymentModuleResourceModel } from "src/app/shared/infrastructure/PortalHttpClient/model/employmentModuleResourceModel";

export interface EmployeePayrollBasicViewStateModel {
    isEmployeePayrollBasicView: boolean;
    activatedParam: number;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employeePayrollBasicView: EmployeePayrollBasicResponse,
    isDisabled: boolean;
    banksView: BanksResponse;
    residencys: Array<{value: string, text: string}>;
    participationInPensions: Array<{value: string, text: string}>;
    grossSalary: number;
    incomeTax: number;
    companyPension: number;
    employeePension: number;
    updateEmployeePayrollBasicModel: EditEmployeePayrollBasicRequest;
    currentRetirement: string;
    errorResources: ErrorResourceModel;
    isLoading: boolean;
    actionPermissions: ActionPermissions;
}