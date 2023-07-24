using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Employee.Models
{
    public class CreateEmployeeRequest
    {
        public EmployeeRoleModel EmployeeRole { get; set; }

        public PersonalInformationModel PersonalInformation { get; set; }
    }

    public class EmployeeRoleModel
    {
        public int RoleId { get; set; }
    }

    public class PersonalInformationModel
    {
        public string FullName { get; set; }

        public string Position { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PersonalId { get; set; }

        public JobType JobType { get; set; }
    }
}
