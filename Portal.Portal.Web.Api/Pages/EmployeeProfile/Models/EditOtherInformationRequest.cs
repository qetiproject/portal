using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditOtherInformationRequest
    {
        public int EmployeeId { get; set; }

        public Gender? Gender { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public string? LegalAddress { get; set; }

        public string? ActualAddress { get; set; }

        public Conviction? Conviction { get; set; }

        public string? FullName { get; set; }

        public string? Relationship { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
