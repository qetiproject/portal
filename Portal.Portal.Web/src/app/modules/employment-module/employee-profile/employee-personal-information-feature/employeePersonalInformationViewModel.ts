import { Subscription } from "rxjs";
import { ActionPermissions, EditEmployeePersonalInformationRequest, EmployeePersonalInformationResponse, EmployeeStatus, EmploymentModuleResourceModel, ErrorResourceModel, JobType } from "src/app/shared/infrastructure/PortalHttpClient";

export interface BreadItem {
    url?: string;
    text: string;
    click?: () => void;
}

interface EmployeeSecondaryInformationTabItemModel {
    title: string,
    path: string
}
export interface EmployeePersonalInformationViewModel {
    isEmployeePersonalInformationView: boolean;
    employmentModuleResourceModel: EmploymentModuleResourceModel;
    employeePersonalInformationView: EmployeePersonalInformationResponse;
    employeeStatus: typeof EmployeeStatus;
    activatedParam: number;
    breadItems: Array<BreadItem>; 
    jobTypes: Array<{value: string, text: string}>;
    jobTypesEnum: typeof JobType;
    employeeSecondaryInformationTabItems: EmployeeSecondaryInformationTabItemModel[];
    employeeSecondaryInformationActiveTab: string;
    selectedFile?: Blob;
    basePath: string;
    errorResourceList: ErrorResourceModel;
    statuses: Array<{value: string, text: string}>;
    isOpenedProfilePhotoDialog: boolean;
    selectedItemIndex: number;
    updateEmployeePersonalInformationModel: EditEmployeePersonalInformationRequest;
    employeeListUrl: Array<string>;
    subscriptions: Array<Subscription>;
    isLoadingOnGet: boolean;
    isLoadingOnPost: boolean;
    actionPermissions: ActionPermissions;
    isHideProfilePhoto: boolean;
}
