import { ActionPermissions, DeleteEmployeeTrainingyRequest, DeleteEmployeeUniversityRequest,  EmployeeEducationResponse, EmployeeTrainingModel, EmployeeUniversityModel, EmploymentModuleResourceModel, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface EmployeeEducationViewModel {
    activatedParam: number;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employmentErrorResourceModel: ErrorResourceModel;
    employeeEducationViewModel: EmployeeEducationResponse;
    isTrainingFormViewStateOpened: boolean;
    isTrainingFormEditViewState: boolean;
    isTrainingDeleteModalOpened: boolean;
    trainingToDelete: EmployeeTrainingModel;
    // addEmployeeTrainingView: AddEmployeeTrainingRequest;
    // editEmployeeTrainingView: EditEmployeeTrainingRequest;
    deleteEmployeeTrainingView: DeleteEmployeeTrainingyRequest;
    isUniversityFormViewStateOpened: boolean;
    isUniversityFormEditViewState: boolean;
    isUniversityDeleteModalOpened: boolean;
    universityToDelete: EmployeeUniversityModel;
    // addEmployeeUniversityView: AddEmployeeUniversityRequest;
    // editEmployeeUniversityView: EditEmployeeUniversityRequest;
    deleteEmployeeUniversityView: DeleteEmployeeUniversityRequest;
    isLoadingOnGet: boolean;
    isLoadingOnPost: boolean;
    actionPermissions: ActionPermissions;
    isMyProfile: boolean;
    certificateSelectedFile: Blob;
    certificateFileExtensionOnUpload: string;
    selectedFilesChanged: boolean;
}
