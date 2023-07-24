import { FileType, MyNonComplianceDetailsResponse, NonComplianceStatus } from "src/app/shared/infrastructure/PortalHttpClient";
import { BreadItem } from "../../employment-module/employee-profile/employee-personal-information-feature/employeePersonalInformationViewModel";

export interface MyNonCompilanceDetailsModel {
    activatedParam: string;
    basePath: string;
    selectedItemIndex: number;
    breadItems: Array<BreadItem>; 
    nonCompilanceListUrl: Array<string>;
    myNonCompilanceDetailsViewModel: MyNonComplianceDetailsResponse;
    fileTypes: typeof FileType;
    addCommentFormViewState: boolean;
    uploadFile: Blob;
    uploadFileExtensionOnUpload: string;
    isMyComment: boolean;
    statusChangeView: boolean;
    statuses: typeof NonComplianceStatus,
    isLoadingOnGet: boolean;
    isLoadingOnPost: boolean;
}