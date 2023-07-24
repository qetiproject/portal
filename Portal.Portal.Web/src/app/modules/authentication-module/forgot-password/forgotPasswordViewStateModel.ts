import { AuthorizationModuleResourceModel, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

interface ServerErrorsModel {
    messages?: Array<string>;
};

export interface ForgotPassowrdViewStateModel {
    authorizationModuleResourceModel: AuthorizationModuleResourceModel;
    errorResourceListModel: ErrorResourceModel;
    serverErrorsModel?: ServerErrorsModel;
    isForgotPasswordButton: boolean;
    userEmail: string;
    isLoadingOnPost: boolean;
}