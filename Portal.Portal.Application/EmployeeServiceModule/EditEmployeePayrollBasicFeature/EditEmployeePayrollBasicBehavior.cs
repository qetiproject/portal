using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeePayrollBasicFeature
{
    public class EditEmployeePayrollBasicBehavior : Behavior<Employee, EditEmployeePayrollBasicAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeePayrollBasicAction action)
        {
            if (rootEntity.PayrollBasic is null)
                rootEntity.PayrollBasic = new PayrollBasic();

            var payrollBasic = rootEntity.PayrollBasic;
            payrollBasic.Bank = action.Bank;
            payrollBasic.BankCode = action.BankCode;
            payrollBasic.AccountNumber = action.AccountNumber;
            payrollBasic.Residency = action.Residency;
            payrollBasic.ParticipationInPension = action.ParticipationInPension;
            payrollBasic.GrossSalary = action.GrossSalary;
            payrollBasic.NetSalary = action.NetSalary;
            payrollBasic.IncomeTax = action.IncomeTax;
            payrollBasic.CompanyPension = action.CompanyPension;
            payrollBasic.EmployeePension = action.EmployeePension;

            //var payrollBasic = new PayrollBasic(
            //    Action.Bank,
            //    Action.BankCode,
            //    Action.AccountNumber,
            //    Action.PhysicalPerson,
            //    Action.Retirement,
            //    Action.GrossSalary,
            //    Action.NetSalary,
            //    Action.IncomeTax,
            //    Action.CompanyPension,
            //    Action.EmployeePension
            //);

            //rootEntity = rootEntity with { PayrollBasic = payrollBasic };

            return new Result<Employee>(rootEntity);
        }
    }
}
