import { ActionPermissions, EditEmployeeJobInfoRequest, EmployeeJobInfoResponse, EmploymentModuleResourceModel, FileType, TimeZonesResponse } from "src/app/shared/infrastructure/PortalHttpClient";

export interface EmployeeJobInfoViewStateModel {
    isEmployeeJobInfoView: boolean;
    basePath: string;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employeeJobInfoViewModel: EmployeeJobInfoResponse;
    timeZonesResourceModel: TimeZonesResponse;
    activatedParam: number;
    editEmployeeJobInfoViewModel: EditEmployeeJobInfoRequest;
    fileTypes: typeof FileType;
    idDocumentSelectedFile: Blob;
    idDocumentFileExtensionOnUpload: string;
    isLoading: boolean;
    selectedFilesChanged: boolean;
    actionPermissions: ActionPermissions;
}
