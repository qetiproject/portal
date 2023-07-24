using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.ApplicationUsers.CreateUserFeature;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.ImportEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule
{
    public class ImportEmployeeBusinessProcess : BusinessProcess
    {
        private readonly ImportEmployeeBehavior importEmployeeBehavior;
        private readonly CreateUserBehavior createEmployeeUserBehavior;

        public ImportEmployeeBusinessProcess(UserManager<User> userManager)
            : this(
                        new ImportEmployeeBehavior(),
                        new CreateUserBehavior(userManager)
                  )
        { }

        public ImportEmployeeBusinessProcess(
            ImportEmployeeBehavior addEmployeeBehavior,
            CreateUserBehavior createEmployeeUserBehavior
            )
        {
            this.importEmployeeBehavior = addEmployeeBehavior;
            this.createEmployeeUserBehavior = createEmployeeUserBehavior;
        }

        public override Result<IEntity[]> Behave(IContent<IEntity> entityContent, Common.Action action)
        {
            ImportEmployeeBusinessProcessAction importEmployeeBusinessProcessAction = (ImportEmployeeBusinessProcessAction)action;

            var importEmployeeAction = new ImportEmployeeAction(
                importEmployeeBusinessProcessAction.FullName,
                importEmployeeBusinessProcessAction.Position,
                importEmployeeBusinessProcessAction.PhoneNumber,
                importEmployeeBusinessProcessAction.Email,
                importEmployeeBusinessProcessAction.DateOfBirth,
                importEmployeeBusinessProcessAction.PersonalId,
                importEmployeeBusinessProcessAction.JobType,
                importEmployeeBusinessProcessAction.Region,
                importEmployeeBusinessProcessAction.WorkAddresss,
                importEmployeeBusinessProcessAction.IdExpirationDate,
                importEmployeeBusinessProcessAction.Department,
                importEmployeeBusinessProcessAction.Bank,
                importEmployeeBusinessProcessAction.AccountNumber,
                importEmployeeBusinessProcessAction.Residency,
                importEmployeeBusinessProcessAction.ParticipationInPension,
                importEmployeeBusinessProcessAction.NetSallary,
                importEmployeeBusinessProcessAction.JobStartDate,
                importEmployeeBusinessProcessAction.Gender,
                importEmployeeBusinessProcessAction.MaritalStatus,
                importEmployeeBusinessProcessAction.ActualAdress,
                importEmployeeBusinessProcessAction.LegalAddress,
                importEmployeeBusinessProcessAction.Conviction,
                importEmployeeBusinessProcessAction.EmergencyContactFullName,
                importEmployeeBusinessProcessAction.EmergencyContactRelationship,
                importEmployeeBusinessProcessAction.EmergencyContactPhoneNumber
            );

            var employeeNewState = new NextState(importEmployeeBehavior).Handle(entityContent, importEmployeeAction);

            if (!employeeNewState.Ok) return employeeNewState;

            var createUserAction = new CreateUserAction(
                importEmployeeBusinessProcessAction.Email,
                importEmployeeBusinessProcessAction.PhoneNumber
            );

            var userNewState = new NextState(createEmployeeUserBehavior).Handle(entityContent, createUserAction);

            if (!userNewState.Ok) return userNewState;

            return new Result<IEntity[]>(employeeNewState.Value.Concat(userNewState.Value).ToArray());
        }
    }



    public class ImportEmployeeBusinessProcessAction : Common.Action
    {
        public ImportEmployeeBusinessProcessAction(
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
