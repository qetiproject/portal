import { ActionPermissions, EmergencyContactModel, EmploymentModuleResourceModel, ErrorResourceModel, FileType, OtherInformationModel } from "src/app/shared/infrastructure/PortalHttpClient";
import { EditOtherInformationRequest } from "src/app/shared/infrastructure/PortalHttpClient/model/editOtherInformationRequest";

export interface EmployeeOtherInfoViewStateModel {
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employmentErrorResourceModel: ErrorResourceModel;
    activatedParam: number;
    isEmployeeOtherInfoViewState: boolean;
    employeeOtherInfoViewModel: OtherInformationModel;
    employeeEmergencyContactViewModel: EmergencyContactModel;
    genders: Array<{value: string, text: string}>;
    maritalStatuses: Array<{value: string, text: string}>;
    convictions: Array<{value: string, text: string}>;
    bloodGroupSelectedFile: Blob;
    bloodGroupFileExtensionOnUpload: string;
    medicalCertificateSelectedFile: Blob;
    medicalCertificateFileExtensionOnUpload: string;
    alergiesSelectedFile: Blob;
    alergiesFileExtensionOnUpload: string;
    drivingLicenseSelectedFile: Blob;
    drivingLicenseFileExtensionOnUpload: string;
    updateEmployeeOtherInfoForm: EditOtherInformationRequest;
    basePath: string;
    isFilesUpload: boolean;
    fileTypes: typeof FileType;
    isLoading: boolean;
    selectedFilesChanged: boolean;
    actionPermissions: ActionPermissions;
}
