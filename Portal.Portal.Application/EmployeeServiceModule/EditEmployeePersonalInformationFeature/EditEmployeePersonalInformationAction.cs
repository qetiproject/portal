using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeePersonalInformationFeature
{
    [Validate(typeof(EditEmployeePersonalInformationActionBusinessRules))]
    public class EditEmployeePersonalInformationAction : Common.Action
    {
        public EditEmployeePersonalInformationAction(
            object id,
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            string personalId,
            EmployeeStatus status,
            JobType jobType)
        {
            Id = id;
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalId;
            Status = status;
            JobType = jobType;
        }

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
