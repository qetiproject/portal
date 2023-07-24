import { AllNonComplianceDetailsResponse, ErrorResourceModel, FileType, NonComplianceResourcesModel, NonComplianceStatus } from "src/app/shared/infrastructure/PortalHttpClient";

import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface AllNonCompilanceModuleDetailsModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    nonCompilanceListUrl: Array<string>;
    loading: boolean;
    nonCompilanceAllDetailsViewModel: AllNonComplianceDetailsResponse;
    fileTypes: typeof FileType;
    statusChangeDialogActionsViewState: boolean;
    statuses: typeof NonComplianceStatus,
    rightOfStatusChangeHiddenField: boolean,
    isDisabledOngoingButton: boolean;
    redirectRequestViewState: boolean;
    openStatusChangeDialogViewState: boolean;
    loaderVisible: boolean;
    statusChangeSelectedFile: Blob;
    statusChangeSelectedFileExtensionOnUpload: string;
    statusChangeView: boolean;
    initialRecievers: Array<string>;
    receivers: Array<string>;
    isViolatorResponse: boolean;
}