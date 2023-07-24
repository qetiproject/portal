using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature
{
    [Validate(typeof(CreateEmployeeActionBusinessRules))]
    public class CreateEmployeeAction : Common.Action
    {
        public CreateEmployeeAction(
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime birthDate,
            string personalId,
            JobType jobYype,
            int roleId)
        {
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = birthDate;
            PersonalId = personalId;
            JobType = jobYype;
            RoleId = roleId;
        }

        public string FullName { get; }

        public string Position { get; }

        public string PhoneNumber { get; }

        public string Email { get; }

        public DateTime DateOfBirth { get; }

        public string PersonalId { get; }

        public JobType JobType { get; }

        public int RoleId { get; set; }
    }
}
