import { InputRounded } from "@progress/kendo-angular-inputs";

import { AuthorizationModuleResourceModel, ErrorResourceModel } from "src/app/shared/infrastructure/PortalHttpClient";

interface ServerErrorsModel {
    messages?: Array<string>;
};

export interface LoginFeatureViewStateModel {
    authorizationModuleResourceModel: AuthorizationModuleResourceModel;
    errorResourceListModel: ErrorResourceModel;
    roundedModel: InputRounded;
    serverErrorsModel?: ServerErrorsModel;
    inputPasswordType: string;
    inputTextType: string;
}