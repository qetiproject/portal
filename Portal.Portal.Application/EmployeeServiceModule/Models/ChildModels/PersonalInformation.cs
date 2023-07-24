using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record PersonalInformation : Record, IEntity
    {
        public PersonalInformation()
        {

        }

        public PersonalInformation(
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            string personalNumber,
            JobType jobType,
            EmployeeStatus status = EmployeeStatus.Active)
        {
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalNumber;
            JobType = jobType;
            Status = status;
            EmployeeNumber = "";
        }

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
