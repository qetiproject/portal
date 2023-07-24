import { ActionPermissions, AddFormerPositionRequest, DeleteFormerPositionRequest, EditEmploymentHistoryRequest, EditFormerPositionRequest, EmployeeRolesListModel, EmploymentHistoryResponse, EmploymentModuleResourceModel, ErrorResourceModel, FileType, FormerPositionModel, SupervisorEmployeesModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface employeeEmploymentHistoryVIewModel {
    activatedParam: number;
    basePath: string;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employmentErrorResourceModel: ErrorResourceModel;
    employeeEmploymentHistoryViewModel: EmploymentHistoryResponse;
    isEmployeeEmploymentHistoryView: boolean;
    editEmployeeEmploymentHistoryViewModel: EditEmploymentHistoryRequest;
    contractSelectedFile: Blob;
    contractFileExtensionOnUpload: string;
    isFormerPositionFormViewStateOpened: boolean;
    isFormerPositionFormEditViewState: boolean;
    isFormerPositionDeleteModalOpened: boolean;
    formerPositionToDelete: FormerPositionModel;
    addEmployeeFormerPositionView: AddFormerPositionRequest;
    editEmployeeFormerPositionView: EditFormerPositionRequest;
    deleteEmployeeFormerPositionView: DeleteFormerPositionRequest;
    monthsResourceModel: string[];
    fileTypes: typeof FileType;
    isLoadingOnGet: boolean;
    isLoadingOnPost: boolean;
    selectedFilesChanged: boolean;
    initialSupervisor: Array<SupervisorEmployeesModel>;
    supervisorDataView: Array<SupervisorEmployeesModel>;
    contractTypes: string[];
    employeeRoles: Array<EmployeeRolesListModel>;
    employeeRoleName: string,
    employeeRoleId: number;
    actionPermissions: ActionPermissions;
    isEmployeeRoleView: boolean;
    isLoadingOnPostRole: boolean;
}
