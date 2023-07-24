import { Component, Inject, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { PageChangeEvent, RowClassArgs } from '@progress/kendo-angular-grid';

import { BASE_PATH, EmployeeService, EmployeeStatus } from 'src/app/shared/infrastructure/PortalHttpClient';

import { EmployeeModuleViewStateModel } from './employmentModuleViewStateModel';

import { CancelButtonClickEvent, CreateEmployeeButtonClickEvent, EventBus, OkButtonClickEvent } from 'src/app/shared/infrastructure/CustomApi/event.bus';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';

@Component({
  selector: 'app-employment-module',
  templateUrl: './employment-module.component.html',
  styleUrls: ['./employment-module.component.scss'],
})
export class EmploymentModuleComponent implements OnInit {  

  viewState: EmployeeModuleViewStateModel = {
    employmentModuleResourceModel: {},
    initialEmployeeListView: {
      employees: [],
      total: 0,
    },
    employeeListView: {
      employees: [],
      total: 0,
    },
    statusEnum: EmployeeStatus,
    isCreateEmployeeFeature: false,
    gridList: {
      variablesForPageSizes: {
        buttonCount: 5,
        pageSizes: [5, 10, 20, 50],
        pageSize: 10,
        skip: 0,
      },
      sortable: true,
      filter: true,
    },
    searchEmployeeValue: '',
    rounded: 'large',
    buttonSize: 'large',
    basePath: '/',
    createEmployeeButtonItems: [],
    subscriptions: [],
    isLoadingOnGet: false,
    actionPermissions: {},
  };

  constructor(
    private globalResourceService: GlobalResourceService,
    private employeeService: EmployeeService,
    private eventBus: EventBus,
    @Inject(BASE_PATH) basePath: string,
  ) {
    if (basePath) {
      this.viewState.basePath = `${basePath}/`
    }
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  }

  ngOnInit(): void {
    this.loadResource();
    this.employeeListView(this.viewState.searchEmployeeValue);
    this.subscribe();
  }

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.createEmployeeButtonItems = [
      { 
        text: 'Add Individually',
        icon: 'add',
        click: this.createEmployeeButtonClickEvent.bind(this)
      },
      { 
        text: 'Import from Excel',
        icon: 'import',
        click: this.importEmployeeFromExcelClickEvent.bind(this)
      }
    ]

  }

  rowStyling = (context: RowClassArgs) => {
    if (context.index %2 !=0) {
      return { grey: true };
    } else {
      return { white: true };
    }
  };

  employeeFilterInputClickEvent(input: Event): void {
    const inputValue = (input.target as HTMLInputElement).value.toLowerCase();
    this.viewState.employeeListView.employees = this.viewState.initialEmployeeListView.employees.filter((element): any => {
      if(element.fullName.toLowerCase().includes(inputValue) || element.position.toLowerCase().includes(inputValue) || element.phoneNumber.toString().includes(inputValue) || element.email.toLowerCase().includes(inputValue) || element.department?.toLowerCase().includes(inputValue) || element.contractType?.toLowerCase().includes(inputValue) || element.status.toLowerCase().includes(inputValue)) {
        return element;
      }
    });
  }

  employeePageChangeClickEvent({ skip, take }: PageChangeEvent): void {
    this.viewState.gridList.variablesForPageSizes.skip = skip;
    this.viewState.gridList.variablesForPageSizes.pageSize = take;
  }

  async employeeListView(searchEmployee: string): Promise<void> {
    try {
      this.viewState.isLoadingOnGet = true;
      const response = await firstValueFrom(this.employeeService.apiEmployeeEmployeelistGet(searchEmployee));
      if (response.ok) {
        this.viewState.isLoadingOnGet = false;
        this.viewState.initialEmployeeListView.employees = response.value.employees;
        this.viewState.initialEmployeeListView.total = response.value.total;
        this.viewState.employeeListView.employees = response.value.employees;
        this.viewState.employeeListView.total = response.value.total;
      } else {
        this.viewState.isLoadingOnGet = false;
      }
    } catch (error) {
      this.viewState.isLoadingOnGet = false;
      console.error(error);
    }
  }

  createEmployeeButtonClickEvent(): void {
    this.viewState.isCreateEmployeeFeature = true;
    this.eventBus.send(new CreateEmployeeButtonClickEvent());
  }

  importEmployeeFromExcelClickEvent() {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.xlsx';
    input.onchange = async (event: Event) => {
      const file = (event.target as HTMLInputElement).files[0];
      const response = await firstValueFrom(this.employeeService.apiEmployeeImportemployeesPostForm(file))
      if(response.ok) {
        this.eventBus.send(new OkButtonClickEvent());
      }
    };
    input.click();
  }

  okButtonClickEventHandler = (message: OkButtonClickEvent): void => {
    this.viewState.isCreateEmployeeFeature = false;
    this.employeeListView(this.viewState.searchEmployeeValue);
  };

  cancelButtonClickEventHandler = (message: CancelButtonClickEvent): void => {
    this.viewState.isCreateEmployeeFeature = false;
  };

  subscribe() {
    this.viewState.subscriptions = [
      this.eventBus.subscribe<CancelButtonClickEvent>(CancelButtonClickEvent, this.cancelButtonClickEventHandler),
      this.eventBus.subscribe<OkButtonClickEvent>(OkButtonClickEvent, this.okButtonClickEventHandler),
    ];
  };

  unSubscribe() {
    this.viewState.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

}
