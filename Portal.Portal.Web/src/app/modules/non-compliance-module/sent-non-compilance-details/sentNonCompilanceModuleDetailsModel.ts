import { AllNonComplianceDetailsResponse, ErrorResourceModel, FileType, NonComplianceResourcesModel, NonComplianceStatus } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface SentNonCompilanceModuleDetailsModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    nonCompilanceListUrl: Array<string>;
    isLoadingOnGet: boolean;
    nonCompilanceSentDetailsViewModel: AllNonComplianceDetailsResponse;
    fileTypes: typeof FileType;
    statuses: typeof NonComplianceStatus,
    statusChangeView: boolean;
}