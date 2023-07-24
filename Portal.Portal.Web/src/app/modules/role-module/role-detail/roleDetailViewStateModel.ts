import { ButtonSize } from "@progress/kendo-angular-buttons";
import { InputRounded } from "@progress/kendo-angular-inputs";

import { PermissionsResponse, RoleDetailsResponse } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

import { GridListModel } from "src/app/shared/infrastructure/Model/gridListModel";

export interface RoleDetailViewStateModel {
    tabSelectEventTitle: string;
    activatedParam: number;
    roleDetailsResponse: RoleDetailsResponse;
    isLoadingOnGet: boolean;
    isEditRolePermissionView: boolean;
    isLoadingPermissions: boolean,
    isLoadingOnPost: boolean;
    breadItems: Array<BreadItem>; 
    selectedItemIndex: number;
    rolesUrl: Array<string>;
    rounded: InputRounded;
    buttonSize: ButtonSize;
    roundedSearch: InputRounded;
    gridList: GridListModel;
    searchEmployeeValue: string;
    isEmployeesView: boolean;
    isActiveAddEmployeeViewState: boolean;
    employeeDataView: Array<string>;
    activePermissionsTab: boolean;
    activeEmployeesTab: boolean;
    isActiveDeleteEmployeeViewState: boolean;
    employeeId: number;
    permissionsResponse: PermissionsResponse;
    employeeFullName: string;
}