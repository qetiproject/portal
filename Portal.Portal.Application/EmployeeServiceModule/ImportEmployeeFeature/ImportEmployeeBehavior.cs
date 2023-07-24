using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Application.EmployeeServiceModule.ImportEmployeeFeature
{
    public class ImportEmployeeBehavior : Behavior<Employee, ImportEmployeeAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, ImportEmployeeAction action)
        {
            var personalInformation = new PersonalInformation(
                action.FullName,
                action.Position,
                action.PhoneNumber,
                action.Email,
                action.DateOfBirth,
                action.PersonalId,
                action.JobType);

            var jobInfo = new JobInfo(
                action.Region,
                action.WorkAddresss,
                null,
                action.IdExpirationDate,
                action.Department,
                null);

            var payrollModel = Calculate(action.ParticipationInPension, action.NetSallary);

            var payrollBasic = new PayrollBasic(
                action.Bank,
                GetDescription(action.Bank),
                action.AccountNumber,
                action.Residency,
                action.ParticipationInPension,
                payrollModel?.GrossSalary,
                action.NetSallary,
                payrollModel?.IncomeTax,
                payrollModel?.CompanyPension,
                payrollModel?.EmployeePension);

            var employmentHistory = new EmploymentHistory(
                action.JobStartDate);

            var otherInfo = new OtherInformation(
                action.Gender,
                action.MaritalStatus,
                action.LegalAddress,
                action.ActualAdress,
                null,
                null,
                null,
                null,
                action.Conviction);

            var emergencyContact = new EmergencyContact(
                action.EmergencyContactFullName,
                action.EmergencyContactRelationship,
                action.EmergencyContactPhoneNumber);

            var employee = new Employee(
                personalInformation,
                payrollBasic,
                jobInfo,
                otherInfo,
                employmentHistory,
                emergencyContact);

            return new Result<Employee>(employee);
        }

        public PayrollModel Calculate(ParticipationInPension? participationInPension, decimal? netSallary)
        {
            if (participationInPension == null || netSallary == null) return null;

            if (participationInPension == ParticipationInPension.Yes)
            {
                return new PayrollModel(
                Math.Round(netSallary.Value / 0.8m / 0.98m, 2, MidpointRounding.AwayFromZero),
                Math.Round(netSallary.Value / 0.8m * 0.2m, 2, MidpointRounding.AwayFromZero),
                Math.Round(netSallary.Value / 0.8m / 0.98m * 0.02m, 2, MidpointRounding.AwayFromZero),
                Math.Round(netSallary.Value / 0.8m / 0.98m * 0.02m, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                return new PayrollModel(
                Math.Round(netSallary.Value / 0.8m / 0.98m, 2, MidpointRounding.AwayFromZero),
                Math.Round(netSallary.Value / 0.8m * 0.2m, 2, MidpointRounding.AwayFromZero),
                0,
                0);
            }
        }

        public string GetDescription(Enum value)
        {
            if (value is null)
                return string.Empty;

            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

    public class PayrollModel
    {
        public PayrollModel(
            decimal? grossSalary,
            decimal? incomeTax,
            decimal? companyPension,
            decimal? employeePension)
        {
            GrossSalary = grossSalary;
            IncomeTax = incomeTax;
            CompanyPension = companyPension;
            EmployeePension = employeePension;
        }

        public decimal? GrossSalary { get; set; }

        public decimal? IncomeTax { get; set; }

        public decimal? CompanyPension { get; set; }

        public decimal? EmployeePension { get; set; }
    }
}
