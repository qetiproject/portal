using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.ApplicationUsers.CreateUserFeature;
using Portal.Portal.Application.ApplicationUsers.EditUserFeature;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeePersonalInformationFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule
{
    public class EditEmployeePersonalInformationBusinessProcess : BusinessProcess
    {
        private readonly EditEmployeePersonalInformationBehavior editEmployeePersonalInformationBehavior;
        private readonly EditUserBehavior createEmployeeUserBehavior;

        public EditEmployeePersonalInformationBusinessProcess(UserManager<User> userManager)
            : this(
                        new EditEmployeePersonalInformationBehavior(),
                        new EditUserBehavior(userManager)
                  )
        { }

        public EditEmployeePersonalInformationBusinessProcess(
            EditEmployeePersonalInformationBehavior editEmployeePersonalInformationBehavior,
            EditUserBehavior createEmployeeUserBehavior
            )
        {
            this.editEmployeePersonalInformationBehavior = editEmployeePersonalInformationBehavior;
            this.createEmployeeUserBehavior = createEmployeeUserBehavior;
        }

        public override Result<IEntity[]> Behave(IContent<IEntity> entityContent, Common.Action action)
        {
            EditEmployeePersonalInformationBusinessProcessAction editEmployeePersonalInformationBusinessProcessAction = (EditEmployeePersonalInformationBusinessProcessAction)action;

            var editEmployeePersonalInformationAction = new EditEmployeePersonalInformationAction(
                editEmployeePersonalInformationBusinessProcessAction.Id,
                editEmployeePersonalInformationBusinessProcessAction.FullName,
                editEmployeePersonalInformationBusinessProcessAction.Position,
                editEmployeePersonalInformationBusinessProcessAction.PhoneNumber,
                editEmployeePersonalInformationBusinessProcessAction.Email,
                editEmployeePersonalInformationBusinessProcessAction.DateOfBirth,
                editEmployeePersonalInformationBusinessProcessAction.PersonalId,
                editEmployeePersonalInformationBusinessProcessAction.Status,
                editEmployeePersonalInformationBusinessProcessAction.JobType
            );

            var employeeNewState = new NextState(editEmployeePersonalInformationBehavior).Handle(entityContent, editEmployeePersonalInformationAction);

            if (!employeeNewState.Ok) return employeeNewState;

            var editUserAction = new EditUserAction(
                editEmployeePersonalInformationBusinessProcessAction.UserId,
                editEmployeePersonalInformationBusinessProcessAction.Email,
                editEmployeePersonalInformationBusinessProcessAction.PhoneNumber
            );

            var userNewState = new NextState(createEmployeeUserBehavior).Handle(entityContent, editUserAction);

            if (!userNewState.Ok) return userNewState;

            return new Result<IEntity[]>(employeeNewState.Value.Concat(userNewState.Value).ToArray());
        }
    }



    public class EditEmployeePersonalInformationBusinessProcessAction : Common.Action
    {
        public EditEmployeePersonalInformationBusinessProcessAction(
            int id,
            int userId,
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
            UserId = userId;
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalId;
            Status = status;
            JobType = jobType;
        }

        public int UserId { get; set; }

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
