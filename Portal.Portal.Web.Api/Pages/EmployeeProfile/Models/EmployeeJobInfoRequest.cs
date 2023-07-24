using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmployeeJobInfoRequest
    {
        public int Id { get; set; }
    }

    public class EmployeeJobInfoResponse
    {
        public int Id { get; set; }

        public string? Region { get; set; }

        public string? WorkAddress { get; set; }

        public EmployeeFileModel? IdDocument { get; set; }

        public DateTime? IdExpirationDate { get; set; }

        public string? Department { get; set; }

        public TimeZoneModel? TimeZone { get; set; }
    }
}
