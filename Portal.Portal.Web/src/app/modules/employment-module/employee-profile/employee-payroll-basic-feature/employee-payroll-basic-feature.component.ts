import { firstValueFrom } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Residency } from 'src/app/shared/infrastructure/PortalHttpClient/model/residency';
import { ParticipationInPension } from 'src/app/shared/infrastructure/PortalHttpClient';
import { BankModel } from 'src/app/shared/infrastructure/PortalHttpClient/model/bankModel';

import { EmployeePayrollBasicViewStateModel } from './employeePayrollBasicViewStateModel';

import { EmployeeProfileService } from 'src/app/shared/infrastructure/PortalHttpClient';

import { GlobalResourceService } from 'src/app/shared/infrastructure/CustomApi/global-resource.service';
import { enumToArray } from 'src/app/shared/infrastructure/Utils/utils';

@Component({
  selector: 'app-employee-payroll-basic-feature',
  templateUrl: './employee-payroll-basic-feature.component.html',
  styleUrls: ['./employee-payroll-basic-feature.component.scss']
})
export class EmployeePayrollBasicFeatureComponent implements OnInit{  
  viewState: EmployeePayrollBasicViewStateModel = {
    isEmployeePayrollBasicView: true,
    activatedParam: null,
    employmentModuleResourceModel: {},
    employeePayrollBasicView: {},
    isDisabled: true,
    banksView: {},
    residencys: null,
    participationInPensions: null,
    grossSalary: 0,
    incomeTax: 0,
    companyPension: 0,
    employeePension: 0,
    updateEmployeePayrollBasicModel: {},
    currentRetirement: ParticipationInPension.No,
    errorResources: {},
    isLoading: false,
    actionPermissions: {},
  };

  employeePayrollBasicForm = new FormGroup({
    bank: new FormControl(null),
    bankCode: new FormControl(''),
    accountNumber: new FormControl(''),
    residency: new FormControl(''),
    participationInPension: new FormControl(null),
    grossSalary: new FormControl(null),
    netSalary: new FormControl(null),
    incomeTax: new FormControl(null),
    companyPension: new FormControl(null),
    employeePension: new FormControl(null),
  });

  constructor(
    private router: Router,
    private globalResourceService: GlobalResourceService,
    private employeeProfileService: EmployeeProfileService
  ) {
    this.viewState.activatedParam = +this.router.url.split('/')[4];
    const user = JSON.parse(localStorage.getItem('user'));
    this.viewState.actionPermissions = user.actionPermissions;
  };

  ngOnInit(): void {
    this.loadResource();
    this.employeePayrollBasicView(this.viewState.activatedParam);
  };

  loadResource(): void {
    this.viewState.employmentModuleResourceModel = this.globalResourceService.resourceResponse.employmentModuleResources;
    this.viewState.errorResources = this.globalResourceService.resourceResponse.errorResources;
    this.viewState.residencys = enumToArray(Residency, this.viewState.employmentModuleResourceModel);
    this.viewState.participationInPensions = enumToArray(ParticipationInPension, this.viewState.employmentModuleResourceModel);
  }

  async employeePayrollBasicView(id: number) {
    try{
     this.viewState.isLoading = true;
     const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmployeepayrollbasicGet(id));
     if(response.ok) {
       this.viewState.isLoading = false;
       this.viewState.employeePayrollBasicView = response.value;
      } else {
        this.viewState.isLoading = false;
      }
    }catch(error){
      this.viewState.isLoading = false;
     console.log(error);
    }
  };

  async employeeProfileBankData() {
    const response  = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileBanksGet());
    if(response) {
      this.viewState.banksView = response;
    }
  };

  async employeesalarycalculation(participationInPension: ParticipationInPension, netSalary: number) {
    try{
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEmployeesalarycalculationGet(participationInPension, netSalary));

      if(response.ok) {
        this.employeePayrollBasicForm.get('grossSalary').setValue(response.value.grossSalary);
        this.employeePayrollBasicForm.get('incomeTax').setValue(response.value.incomeTax);
        this.employeePayrollBasicForm.get('companyPension').setValue(response.value.companyPension);
        this.employeePayrollBasicForm.get('employeePension').setValue(response.value.employeePension);
        this.viewState.grossSalary = response.value.grossSalary;
        this.viewState.incomeTax = response.value.incomeTax;
        this.viewState.companyPension = response.value.companyPension;
        this.viewState.employeePension = response.value.employeePension;
      }
    }catch(error) {
      console.log(error);
    }
  };

  employeePayrollBasicEditButtonClickEvent() {
    this.viewState.isEmployeePayrollBasicView = false;
    this.employeeProfileBankData();
    this.employeePayrollBasicForm.controls['bankCode'].disable();
    this.employeePayrollBasicForm.controls['grossSalary'].disable();
    this.employeePayrollBasicForm.controls['incomeTax'].disable();
    this.employeePayrollBasicForm.controls['companyPension'].disable();
    this.employeePayrollBasicForm.controls['employeePension'].disable();

    this.viewState.currentRetirement = this.viewState.employeePayrollBasicView.participationInPension;

    this.employeePayrollBasicForm.setValue({
      bank: this.viewState.employeePayrollBasicView.bank,
      bankCode: this.viewState.employeePayrollBasicView.bankCode,
      accountNumber: this.viewState.employeePayrollBasicView.accountNumber,
      residency: this.viewState.employeePayrollBasicView.residency,
      participationInPension: this.viewState.currentRetirement,
      netSalary: this.viewState.employeePayrollBasicView.netSalary,
      grossSalary: this.viewState.employeePayrollBasicView.grossSalary,
      incomeTax: this.viewState.employeePayrollBasicView.incomeTax,
      companyPension: this.viewState.employeePayrollBasicView.companyPension,
      employeePension: this.viewState.employeePayrollBasicView.employeePension,
    })
  };

  netSalaryValueChangeClickEvent() {
    if(this.employeePayrollBasicForm.value.netSalary > 0) {
      this.employeesalarycalculation(this.employeePayrollBasicForm.value.participationInPension, this.employeePayrollBasicForm.value.netSalary)
    }
  };

  participationInPensionChangeEvent(value: string) {
    if(this.viewState.currentRetirement != value) {
      this.employeesalarycalculation(this.employeePayrollBasicForm.value.participationInPension, this.employeePayrollBasicForm.value.netSalary)
    }
  };

  async saveEmployeePayrollBasicButtonClickEvent(employeePayrollBasicFormValue) {
    this.viewState.updateEmployeePayrollBasicModel = {
      id: this.viewState.employeePayrollBasicView.id,
      bank: employeePayrollBasicFormValue.bank.bank,
      bankCode: employeePayrollBasicFormValue.bank.code,
      accountNumber: employeePayrollBasicFormValue.accountNumber,
      residency: employeePayrollBasicFormValue.residency,
      participationInPension: employeePayrollBasicFormValue.participationInPension,
      grossSalary: this.viewState.grossSalary,
      netSalary: employeePayrollBasicFormValue.netSalary,
      incomeTax: this.viewState.incomeTax,
      companyPension: this.viewState.companyPension,
      employeePension: this.viewState.employeePension,
    };

    try {
      this.viewState.isLoading = true;
      const response = await firstValueFrom(this.employeeProfileService.apiEmployeeprofileEditemployeepayrollbasicPost(this.viewState.updateEmployeePayrollBasicModel));
      
      if(response.ok) {
        this.viewState.isEmployeePayrollBasicView = true;
        this.employeePayrollBasicView(this.viewState.activatedParam);
      } else {
      this.viewState.isLoading = false;
      }
    } catch(error) {
      this.viewState.isLoading = false;
      console.log(error);
    }
  };

  cancelEmployeePayrollBasicButtonClickEvent() {
    this.viewState.isEmployeePayrollBasicView = true;
  };

  employeePayrollBasicFormValueChecker(obj): boolean {
    for (let key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] !== null && obj[key] !== "" && obj[key]?.['bank'] !== "") {
        return false;
      }
    }
    return true;
  }

}
