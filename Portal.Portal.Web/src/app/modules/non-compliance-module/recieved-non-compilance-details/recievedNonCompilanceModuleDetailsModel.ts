import { FileType, NonComplianceStatus, ReceivedNonComplianceDetailsResponse } from "src/app/shared/infrastructure/PortalHttpClient";
import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface RecievedNonCompilanceModuleDetailsModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    nonCompilanceListUrl: Array<string>;
    loading: boolean;
    nonCompilanceRecievedDetailsViewModel: ReceivedNonComplianceDetailsResponse;
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
    initialParticipants: Array<string>;
    recievers: Array<string>;
}