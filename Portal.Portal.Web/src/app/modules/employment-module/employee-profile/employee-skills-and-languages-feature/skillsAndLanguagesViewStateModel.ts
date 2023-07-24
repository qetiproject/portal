import { ActionPermissions, AddComputerSkillRequest, AddLanguageRequest, DeleteComputerSkillRequest, DeleteLanguageRequest, EmploymentModuleResourceModel, ErrorResourceModel, FileType, SkillsAndLanguageResponse } from "src/app/shared/infrastructure/PortalHttpClient";

export interface SkillsAndLanguagesViewStateModel {
    activatedParam: number;
    basePath: string;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employmentErrorResourceModel: ErrorResourceModel;
    isEmployeeSkillsViewState: boolean;
    isEmployeeLanguagesViewState: boolean;
    employeeSkillsAndLanguagesView: SkillsAndLanguageResponse;
    deleteEmployeeSkillModel: DeleteComputerSkillRequest;
    addEmployeeSkillModel: AddComputerSkillRequest;
    addEmployeeLanguageModel: AddLanguageRequest;
    deleteEmployeeLanguageModel: DeleteLanguageRequest;
    isLoadingOnGet: boolean;
    isLoadingOnPostForSkills: boolean;
    isLoadingOnPostForLanguages: boolean;
    fileTypes: typeof FileType;
    isEmployeeResumeViewState: boolean;
    resumeSelectedFile: Blob;
    resumeFileExtensionOnUpload: string;
    selectedFilesChanged: boolean;
    actionPermissions: ActionPermissions;
    isMyProfile: boolean;
}