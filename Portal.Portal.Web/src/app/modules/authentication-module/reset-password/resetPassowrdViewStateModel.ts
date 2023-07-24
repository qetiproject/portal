import { AuthorizationModuleResourceModel, ErrorResourceModel, IEntityErrorModel } from "src/app/shared/infrastructure/PortalHttpClient";

export interface ResetPassowrdViewStateModel {
    authorizationModuleResourceModel: AuthorizationModuleResourceModel;
    errorResourceListModel: ErrorResourceModel;
    serverErrorsModel?: IEntityErrorModel;
    isLoadingOnPost: boolean;
    activatedParam: string;
    inputPasswordType: string;
    inputTextType: string;
    inputconfirmPasswordType: string;
    inputConfirmTextType: string;
    isPasswordReset: boolean;
    resetToken: string;
    resetEmail: string;
}