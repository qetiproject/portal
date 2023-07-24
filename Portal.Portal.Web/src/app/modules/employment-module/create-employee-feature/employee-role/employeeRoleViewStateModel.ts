import { EmployeeRolesListModel, EmploymentModuleResourceModel, RoleDetailsResponse } from "src/app/shared/infrastructure/PortalHttpClient";

export interface EmployeeRoleViewStateModel {
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employeeRoles: Array<EmployeeRolesListModel>;
    roleId: number;
    roleDetailsResponse: RoleDetailsResponse;
    isLoadingOnGet: boolean;
    defaultRoleName: string;
    defaultRoleId: number;
}