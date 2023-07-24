using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmployeePayrollBasicRequest
    {
        public int Id { get; set; }
    }
    public class EmployeePayrollBasicResponse
    {
        public int Id { get; set; }

        public BankModel Bank { get; set; }

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
