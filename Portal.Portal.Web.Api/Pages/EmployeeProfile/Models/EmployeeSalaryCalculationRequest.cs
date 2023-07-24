using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmployeeSalaryCalculationRequest
    {
        public ParticipationInPension Retirement { get; set; }

        public decimal NetSalary { get; set; }
    }

    public class EmployeeSalaryCalculationResponse
    {
        public decimal? GrossSalary { get; set; }

        public decimal? IncomeTax { get; set; }

        public decimal? CompanyPension { get; set; }

        public decimal? EmployeePension { get; set; }
    }
}
