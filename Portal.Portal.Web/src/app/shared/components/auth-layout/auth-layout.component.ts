import { AfterViewInit, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription, firstValueFrom } from 'rxjs';

import { DrawerSelectEvent } from '@progress/kendo-angular-layout';

import { GlobalResourceService } from '../../infrastructure/CustomApi/global-resource.service';

import { AuthService } from '../../infrastructure/CustomApi/auth.service';

import { LayoutViewStateModel } from './layoutViewStateModel';
import { LanguageModel } from '../../infrastructure/Model/languageModel';
import { BASE_PATH, LogInService } from '../../infrastructure/PortalHttpClient';
import { EventBus, UpdateUserPhotoButtonClickEvent } from '../../infrastructure/CustomApi/event.bus';

@Component({
  selector: 'app-auth-layout',
  templateUrl: './auth-layout.component.html',
  styleUrls: ['./auth-layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class AuthLayoutComponent implements OnInit, OnDestroy, AfterViewInit {
  private _mobileQueryListener!: () => void;

  routerSubscribe: Subscription;
  eventBusSubscribeForUserPhotoUpdate: Subscription;

  viewState: LayoutViewStateModel = {
    mobileQuery: {},
    activeParam: null,
    layoutResourceModel: {},
    notificationBadgeAlign: { 
      vertical: "top", 
      horizontal: "start" 
    },
    avatarSrc: '',
    avatarPopupSettings: { 
      align: 'center' 
    },
    navigationItemsView: [],
    avatarClickViewStateData: [],
    isAuthorizedViewState: false,
    basePath: '/',
    actionPermissions: {},
  };

  constructor(
    private changeDetectorRef: ChangeDetectorRef, 
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private authService: AuthService,
    private loginService: LogInService,
    private location: Location,
    private eventBus: EventBus,
    @Inject(BASE_PATH) basePath: string
  ) {
    this.viewState.basePath = `${basePath}/`;
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  }
  
  ngOnInit(): void {
    this.viewState.mobileQuery = window.matchMedia("(max-width: 768px)");
    this._mobileQueryListener = () => this.changeDetectorRef.detectChanges();
    this.viewState.mobileQuery.addListener(this._mobileQueryListener);
    this.layoutModuleResource();
    this.getUserPhoto();
  }  

  ngAfterViewInit(): void {
    this.layoutModuleResource();
    this.subscribe();
  }

  layoutModuleResource(): void {
      this.viewState.layoutResourceModel = this.globalResourceService.resourceResponse.layoutResources;
      this.viewState.navigationItemsView = [
        { text: this.viewState.layoutResourceModel.employeeList, icon: "k-i-list-unordered", path: 'employee-service', isShow: this.viewState.actionPermissions.viewEmployeesList},
        { text: this.viewState.layoutResourceModel.timeOffRequests, icon: "k-i-file-txt" , path: 'time-off-requests', isShow: true},
        { text: this.viewState.layoutResourceModel.nonCompliance, icon: "k-i-warning", path: 'non-compliance', isShow: true},
        { text: this.viewState.layoutResourceModel.roles, icon: "k-i-tell-a-friend", path: 'roles', isShow: this.viewState.actionPermissions.viewRoles},
        { text: this.viewState.layoutResourceModel.parametres, icon: "k-i-gears", path: 'parametres', isShow: this.viewState.actionPermissions.viewAdminPanel},
        // { text: this.viewState.layoutResourceModel.dashboard, icon: "k-i-table", path: ''},
        // { text: this.viewState.layoutResourceModel.schedule, icon: "k-i-calendar", path: ''},
        // { text: this.viewState.layoutResourceModel.log, icon: "k-i-parameter-date-time", path: ''},
        // { text: this.viewState.layoutResourceModel.checklists, icon: "k-i-check-outline", path: ''},
        // { text: this.viewState.layoutResourceModel.tasks, icon: "k-i-paste-plain-text", path: ''},
        // { text: this.viewState.layoutResourceModel.devices, icon: "k-i-toggle-full-screen-mode", path: ''},
        // { text: this.viewState.layoutResourceModel.contacts, icon: "k-i-dictionary-add", path: ''}
      ];

      this.viewState.avatarClickViewStateData =[
        {
          text: JSON.parse(localStorage.getItem('user')).fullName,
          click: this.viewMyProfileClickView.bind(this),
        },
        {
          text: this.viewState.layoutResourceModel.logOut,
          click: this.logOutButtonClickEvent.bind(this)
        },
  
      ];

      this.showHideViewItemPanel();
  };

  showHideViewItemPanel(): void {
    this.viewState.navigationItemsView = this.viewState.navigationItemsView.filter((x) => x.isShow == true )
  }


  async getUserPhoto(): Promise<void> {
    try {
      const response = await firstValueFrom(this.loginService.apiLoginGetuserphotoGet());
      if(response.ok) {
        this.viewState.avatarSrc = `${this.viewState.basePath}${response.value}`;
      }
    } catch(error) {
      console.log(error, 'error')
    }

  };

  viewMyProfileClickView(): void {
    const employeeId = JSON.parse(localStorage.getItem('user')).employeeId;
    this.router.navigateByUrl(`/management/employee-service/employee-profile/${employeeId}`)
  }
  
  subscribe(): void {
    this.eventBusSubscribeForUserPhotoUpdate = this.eventBus.subscribe<UpdateUserPhotoButtonClickEvent>(UpdateUserPhotoButtonClickEvent, this.updateUserPhotoEventHandler)
    if(!this.authService.isAutorizedByButtonClickEvent && this.authService.isAutorized) {
      this.routerSubscribe = this.router.events.subscribe(event => {
        if (event instanceof NavigationEnd && !this.authService.isLoggedOutByButtonClickEvent) {
            this.viewState.activeParam = this.router.url.split('/')[2];
            const activeNavigationItemIndex = this.viewState.navigationItemsView.findIndex(navigationItem => navigationItem.path === this.viewState.activeParam);
            if( this.viewState.navigationItemsView[activeNavigationItemIndex]) {
              this.viewState.navigationItemsView[activeNavigationItemIndex].selected = true;
            }
        }
      });
    } else {
        this.viewState.navigationItemsView[0].selected = true;
    }
  }

  updateUserPhotoEventHandler = (message: UpdateUserPhotoButtonClickEvent): void => {
    this.getUserPhoto();
  };

  navigationItemClickViewState(ev: DrawerSelectEvent): void {
    if(ev.item.path) {
      this.router.navigate([`management/${ev.item.path}`]);
    }
  }

  async languageButtonClickEvent(value): Promise<void> {
    localStorage.setItem('activeLanguage', value);
    await this.location.replaceState(this.location.path()); 
    location.reload();
  }

  logOutButtonClickEvent() {
    this.authService.logout();
  }

  unSubscribe(): void {
    this.viewState.mobileQuery.removeListener(this._mobileQueryListener);
    this.routerSubscribe?.unsubscribe;
    this.eventBusSubscribeForUserPhotoUpdate?.unsubscribe;
  }

  ngOnDestroy(): void {
    this.unSubscribe();
  }  

  private isLanguage(lang: string): boolean {
    const activeLanguage = localStorage.getItem('activeLanguage')

    const defaultLang = LanguageModel.Ka;
    const currentLang = activeLanguage;

    return currentLang ? currentLang === lang : defaultLang === lang;
  }

  get isKa(): boolean {
    return this.isLanguage( LanguageModel.Ka);
  }

  get isEn(): boolean {
    return this.isLanguage( LanguageModel.En);
  }

}
