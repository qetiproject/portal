import { FileType, TimeOffRequestHistoryResponse, TimeOffRequestStatus, TimeOffRequestType } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface TimeOffRequestAllHistoryViewModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    timeOffRequestListUrl: Array<string>;
    timeOffRequestAllHistoryViewModel: TimeOffRequestHistoryResponse;
    timeOffRequestType: typeof TimeOffRequestType;
    fileIcon: boolean;
    statusChangeView: boolean;
    fileTypes: typeof FileType;
    loading: boolean;
    statuses: typeof TimeOffRequestStatus;

}