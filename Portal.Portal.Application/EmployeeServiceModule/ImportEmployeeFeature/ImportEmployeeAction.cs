using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.ImportEmployeeFeature
{
    [Validate(typeof(ImportEmployeeActionBusinessRules))]
    public class ImportEmployeeAction : Common.Action
    {
        public ImportEmployeeAction(
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            string personalId,
            JobType jobType,
            string? region,
            string? workAddresss,
            DateTime? idExpirationDate,
            string? department,
            Bank? bank,
            string? accountNumber,
            Residency? residency,
            ParticipationInPension? participationInPension,
            decimal? netSallary,
            DateTime? jobStartDate,
            Gender? gender,
            MaritalStatus? maritalStatus,
            string? actualAdress,
            string? legalAddress,
            Conviction? conviction,
            string? emergencyContactFullName,
            string? emergencyContactRelationship,
            string? emergencyContactPhoneNumber)
        {
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalId;
            JobType = jobType;
            Region = region;
            WorkAddresss = workAddresss;
            IdExpirationDate = idExpirationDate;
            Department = department;
            Bank = bank;
            AccountNumber = accountNumber;
            Residency = residency;
            ParticipationInPension = participationInPension;
            NetSallary = netSallary;
            JobStartDate = jobStartDate;
            Gender = gender;
            MaritalStatus = maritalStatus;
            ActualAdress = actualAdress;
            LegalAddress = legalAddress;
            Conviction = conviction;
            EmergencyContactFullName = emergencyContactFullName;
            EmergencyContactRelationship = emergencyContactRelationship;
            EmergencyContactPhoneNumber = emergencyContactPhoneNumber;
        }

        public string FullName { get; }

        public string Position { get; }

        public string PhoneNumber { get; }

        public string Email { get; }

        public DateTime DateOfBirth { get; }

        public string PersonalId { get; }

        public JobType JobType { get; }

        public string? Region { get; set; }

        public string? WorkAddresss { get; set; }

        public DateTime? IdExpirationDate { get; set; }

        public string? Department { get; set; }

        public Bank? Bank { get; set; }

        public string? AccountNumber { get; set; }

        public Residency? Residency { get; set; }

        public ParticipationInPension? ParticipationInPension { get; set; }

        public decimal? NetSallary { get; set; }

        public DateTime? JobStartDate { get; set; }

        public Gender? Gender { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public string? ActualAdress { get; set; }

        public string? LegalAddress { get; set; }

        public Conviction? Conviction { get; set; }

        public string? EmergencyContactFullName { get; set; }

        public string? EmergencyContactRelationship { get; set; }

        public string? EmergencyContactPhoneNumber { get; set; }
    }
}
