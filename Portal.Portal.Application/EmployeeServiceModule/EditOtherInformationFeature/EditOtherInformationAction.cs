using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditOtherInformationFeature
{
    public class EditOtherInformationAction : Common.Action
    {
        public EditOtherInformationAction(
            int id,
            Gender? gender,
            MaritalStatus? maritalStatus,
            string? legalAddress,
            string? actualAddress,
            Conviction? conviction,
            string? fullName,
            string? relationship,
            string? phoneNumber)
        {
            Id = id;
            Gender = gender;
            MaritalStatus = maritalStatus;
            LegalAddress = legalAddress;
            ActualAddress = actualAddress;
            Conviction = conviction;
            FullName = fullName;
            Relationship = relationship;
            PhoneNumber = phoneNumber;
        }

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
