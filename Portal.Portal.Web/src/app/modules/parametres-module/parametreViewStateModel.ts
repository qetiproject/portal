import { ContractTypesListModel, GroupsListModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface ParametreViewStateModel {
    isContractTypeViewState: boolean;
    isLoadingOnGet: boolean;
    contractTypesListModel: Array<ContractTypesListModel>;
    isLoadingOnPostForContractType: boolean;
    isLoadingOnPost: boolean;
    basePath: string;
    isGroupsViewState: boolean;
    isLoadingOnPostForGroup: boolean;
    groupsListModel: Array<GroupsListModel>;
    isTimeOffRequestsPanelViewState: boolean;
    hrWhoReceivesTimeOffRequests: string;
    hrWhoReceivesTimeOffRequestList: Array<string>;
}