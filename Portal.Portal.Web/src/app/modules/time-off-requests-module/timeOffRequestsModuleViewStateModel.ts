import { ButtonSize } from "@progress/kendo-angular-buttons";
import { InputRounded } from "@progress/kendo-angular-inputs";
import { GridListModel } from "src/app/shared/infrastructure/Model/gridListModel";

import { ActionPermissions, AllTimeOffRequestsModel, AllTimeOffRequestsResponse, ErrorResourceModel, FileType, ReceivedTimeOffRequestsModel, Recipient, SentTimeOffRequestsModel, TimeOffRequestResourcesModel, TimeOffRequestStatus, TimeOffRequestType } from "src/app/shared/infrastructure/PortalHttpClient";

export interface TypeViewModel {
    text: string;
    value: TimeOffRequestType;
}

export interface TimeOffRequestsModuleViewStateModel {
    basePath: string;
    timeOffRequestModuleResourceModel: TimeOffRequestResourcesModel;
    errorResourceModel: ErrorResourceModel;
    initialAllTimeOffRequests: Array<AllTimeOffRequestsModel>;
    timeOffRequestAllViewModel: AllTimeOffRequestsResponse;
    initialRecievedTimeOffRequests: Array<ReceivedTimeOffRequestsModel>;
    timeOffRequestRecievedViewModel: AllTimeOffRequestsResponse;
    initialSentTimeOffRequests: Array<SentTimeOffRequestsModel>;
    timeOffRequestSentViewModel: AllTimeOffRequestsResponse;
    gridList: GridListModel;
    searchTimeOffRequestValue: string;
    rounded: InputRounded;
    buttonSize: ButtonSize;
    isTimeOffRequestAllViewState: boolean;
    isTimeOffRequestRecievedViewState: boolean;
    isTimeOffRequestSentViewState: boolean;
    isActiveCreateDialogViewState: boolean;
    selectedFile: Blob;
    initialRecieverDataView: Array<string>;
    recieverDataView: Array<string>;
    roundedSearch: InputRounded;
    tabSelectEventTitle: string;
    typesView: Array<TypeViewModel>;
    typeChangeValue: string;
    serverErrorsModel: Array<string>;
    fileTypes: typeof FileType;
    statuses: typeof TimeOffRequestStatus;
    filterRetrieveData: string;
    isLoadingOnGet: boolean;
    isLoadingOnPost: boolean;
    activeAllTab: boolean;
    activeSentTab: boolean;
    activeRecievedTab: boolean;
    openTimeOffRecievedRequestStatusChangeDialogViewState: boolean;
    statusChangeSelectedFile: Blob;
    loaderVisible: boolean;
    activateListItemNumber: string;
    isFromFieldViewState: boolean;
    isFromIncludingFieldViewState: boolean;
    isTitleFieldViewState: boolean;
    recipient: typeof Recipient;
    hasHR: boolean;
    hasSupervisor: boolean;
    isRecieverFieldViewState: boolean;
    actionPermissions: ActionPermissions;
}