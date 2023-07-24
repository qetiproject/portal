using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EditEmployeePersonalInformationRequest
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PersonalId { get; set; }

        public EmployeeStatus Status { get; set; }

        public JobType JobType { get; set; }
    }
}
