namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditEmploymentHistoryRequest
    {
        public int EmployeeId { get; set; }

        public DateTime? JobStartDate { get; set; }

        public DateTime? ContractExpirationDate { get; set; }

        public string? Supervisor { get; set; }

        public string? ContractType { get; set; }
    }
}
