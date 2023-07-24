import { ButtonSize } from "@progress/kendo-angular-buttons";
import { InputRounded } from "@progress/kendo-angular-inputs";

import { GridListModel } from "src/app/shared/infrastructure/Model/gridListModel";

import { PermissionsResponse, ResourceResponse, RolesListModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface RoleViewStateModel {
    errorResourceModel: ResourceResponse;
    rounded: InputRounded;
    buttonSize: ButtonSize;
    roundedSearch: InputRounded;
    isLoadingOnGet: boolean;
    initialRoleListViewModel: Array<RolesListModel>;
    roleListViewModel: Array<RolesListModel>;
    gridList: GridListModel;
    searchRoleValue: string;
    isActiveCreateRoleViewState: boolean;
    isLoadingOnPost: boolean;
    permissionsResponse: PermissionsResponse;
    isLoadingPermissions: boolean;
}