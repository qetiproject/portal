import { ActionPermissions, LayoutResourcesModel } from "../../infrastructure/PortalHttpClient";

interface NavigationItemViewModel {
    text: string;
    icon?: string;
    path?: string;
    selected?: boolean;
    click?: () => void;
    isShow?: boolean;
}

interface AvatarPopupSettingsViewModel {
    align: "left" | "right" | "center";
}

class NotificationBadgeAlignViewModel {
    vertical: "top" | "bottom"; 
    horizontal: "start" | "end"
}

export interface LayoutViewStateModel {
    mobileQuery?: MediaQueryList | any;
    layoutResourceModel: LayoutResourcesModel;
    activeParam: string;
    notificationBadgeAlign: NotificationBadgeAlignViewModel;
    avatarSrc: string;
    avatarPopupSettings: AvatarPopupSettingsViewModel;
    navigationItemsView: Array<NavigationItemViewModel>;
    avatarClickViewStateData: Array<NavigationItemViewModel>;
    isAuthorizedViewState: boolean;
    basePath: string;
    actionPermissions: ActionPermissions;
}