import { FileType, ReceivedTimeOffRequestHistoryResponse, TimeOffRequestStatus, TimeOffRequestType } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface TimeOffRequestRecievedHistoryViewModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    timeOffRequestListUrl: Array<string>;
    timeOffRequestRecievedHistoryViewModel: ReceivedTimeOffRequestHistoryResponse;
    timeOffRequestType: typeof TimeOffRequestType;
    fileIcon: boolean;
    openTimeOffRecievedRequestStatusChangeDialogViewState: boolean;
    statuses:  typeof TimeOffRequestStatus;
    statusChangeSelectedFile: Blob;
    statusChangeSelectedFileExtensionOnUpload: string;
    statusChangeDialogActionsViewState: boolean;
    timeOffRecievedRedirectRequestViewState: boolean;
    initialParticipants: Array<string>;
    participants: Array<string>;
    isDisabledOngoingButton: boolean;
    redirectHistoryStatusChangeView: boolean;
    rightOfConfirmationHiddenField: boolean;
    fileTypes: typeof FileType;
    loading: boolean;
    loaderVisible: boolean;
    filterRetrieveData: string;
}