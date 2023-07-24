import { FileType, SentTimeOffRequestDetailsResponse, TimeOffRequestStatus, TimeOffRequestType } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface TimeOffRequestSentHistoryViewModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    timeOffRequestListUrl: Array<string>;
    timeOffRequestSentHistoryViewModel: SentTimeOffRequestDetailsResponse;
    timeOffRequestType: typeof TimeOffRequestType;
    fileIcon: boolean;
    redirectHistoryStatusChangeView: boolean;
    fileTypes: typeof FileType;
    loading: boolean;
    statuses: typeof TimeOffRequestStatus,
}