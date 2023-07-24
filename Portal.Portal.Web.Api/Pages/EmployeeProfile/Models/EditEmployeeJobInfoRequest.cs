using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditEmployeeJobInfoRequest
    {
        public int Id { get; set; }

        public string? Region { get; set; }

        public string? WorkAddress { get; set; }

        public DateTime? IdExpirationDate { get; set; }

        public string? Department { get; set; }

        public TimeZoneEnum? TimeZone { get; set; }
    }
}
