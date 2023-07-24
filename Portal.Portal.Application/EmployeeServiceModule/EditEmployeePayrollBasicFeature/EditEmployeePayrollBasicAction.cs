using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeePayrollBasicFeature
{
    public class EditEmployeePayrollBasicAction : Common.Action
    {
        public EditEmployeePayrollBasicAction(
            int id,
            Bank? bank,
            string? bankCode,
            string? accountNumber,
            Residency? residency,
            ParticipationInPension? participationInPension,
            decimal? grossSalary,
            decimal? netSalary,
            decimal? incomeTax,
            decimal? companyPension,
            decimal? employeePension)
        {
            Id = id;
            Bank = bank;
            BankCode = bankCode;
            AccountNumber = accountNumber;
            Residency = residency;
            ParticipationInPension = participationInPension;
            GrossSalary = grossSalary;
            NetSalary = netSalary;
            IncomeTax = incomeTax;
            CompanyPension = companyPension;
            EmployeePension = employeePension;
        }

        public Bank? Bank { get; set; }

        public string? BankCode { get; set; }

        public string? AccountNumber { get; set; }

        public Residency? Residency { get; set; }

        public ParticipationInPension? ParticipationInPension { get; set; }

        public decimal? GrossSalary { get; set; }

        public decimal? NetSalary { get; set; }

        public decimal? IncomeTax { get; set; }

        public decimal? CompanyPension { get; set; }

        public decimal? EmployeePension { get; set; }
    }
}
