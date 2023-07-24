import { ButtonSize } from "@progress/kendo-angular-buttons";
import { InputRounded } from "@progress/kendo-angular-inputs";

import { ActionPermissions, EmployeeListResponse, EmployeeStatus, EmploymentModuleResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

import { GridListModel } from "src/app/shared/infrastructure/Model/gridListModel";
import { Subscription } from "rxjs";

interface CreateEmployeeButtonItemModel {
    text: string;
    icon: string;
    click: () => void;
}

export interface EmployeeModuleViewStateModel {
    isCreateEmployeeFeature: boolean;
    initialEmployeeListView: EmployeeListResponse;
    employeeListView: EmployeeListResponse;
    statusEnum: typeof EmployeeStatus;
    gridList: GridListModel;
    searchEmployeeValue: string;
    rounded: InputRounded;
    buttonSize: ButtonSize;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    basePath: string;
    createEmployeeButtonItems: Array<CreateEmployeeButtonItemModel>;
    subscriptions: Array<Subscription>;
    isLoadingOnGet: boolean;
    actionPermissions: ActionPermissions;
}
