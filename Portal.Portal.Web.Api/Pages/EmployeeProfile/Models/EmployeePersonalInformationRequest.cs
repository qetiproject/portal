using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmployeePersonalInformationRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class EmployeePersonalInformationResponse
    {
        public EmployeePersonalInformationResponse(
            int id,
            string photo,
            bool hidden,
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            string personalId,
            EmployeeStatus status,
            JobType jobType,
            string employeeNumber)
        {
            Id = id;
            Photo = photo;
            Hidden = hidden;
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalId;
            Status = status;
            JobType = jobType;
            EmployeeNumber = employeeNumber;
        }

        public int Id { get; set; }

        public string Photo { get; set; }

        public bool Hidden { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PersonalId { get; set; }

        public EmployeeStatus Status { get; set; }

        public JobType JobType { get; set; }

        public string EmployeeNumber { get; set; }
    }
}
