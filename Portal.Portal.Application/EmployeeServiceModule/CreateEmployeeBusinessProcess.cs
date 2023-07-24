using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Portal.Portal.Application.ApplicationUsers.CreateUserFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.RolesModule.AddEmployeeFeature;
using Twilio.Http;

namespace Portal.Portal.Application.EmployeeServiceModule
{
    public class CreateEmployeeBusinessProcess : BusinessProcess
    {
        private readonly CreateEmployeeBehavior addEmployeeBehavior;
        private readonly CreateUserBehavior createEmployeeUserBehavior;
        private readonly AddEmployeeBehavior addEmployeeToRoleBehavior;

        public CreateEmployeeBusinessProcess(UserManager<User> userManager)
            : this(
                        new CreateEmployeeBehavior(),
                        new CreateUserBehavior(userManager),
                        new AddEmployeeBehavior()
                  )
        { }

        public CreateEmployeeBusinessProcess(
            CreateEmployeeBehavior addEmployeeBehavior,
            CreateUserBehavior createEmployeeUserBehavior,
            AddEmployeeBehavior addEmployeeToRoleBehavior
            )
        {
            this.addEmployeeBehavior = addEmployeeBehavior;
            this.createEmployeeUserBehavior = createEmployeeUserBehavior;
            this.addEmployeeToRoleBehavior = addEmployeeToRoleBehavior;
        }

        public override Result<IEntity[]> Behave(IContent<IEntity> entityContent, Common.Action action)
        {
            CreateEmployeeBusinessProcessAction addEmployeeBusinessProcessAction = (CreateEmployeeBusinessProcessAction)action;

            var addEmployeeAction = new CreateEmployeeAction(
                addEmployeeBusinessProcessAction.FullName,
                addEmployeeBusinessProcessAction.Position,
                addEmployeeBusinessProcessAction.PhoneNumber,
                addEmployeeBusinessProcessAction.Email,
                addEmployeeBusinessProcessAction.DateOfBirth,
                addEmployeeBusinessProcessAction.PersonalId,
                addEmployeeBusinessProcessAction.JobType,
                addEmployeeBusinessProcessAction.RoleId
            );

            var employeeNewState = new NextState(addEmployeeBehavior).Handle(entityContent, addEmployeeAction);

            if (!employeeNewState.Ok) return employeeNewState;

            var createUserAction = new CreateUserAction(
                addEmployeeBusinessProcessAction.Email,
                addEmployeeBusinessProcessAction.PhoneNumber
            );

            var userNewState = new NextState(createEmployeeUserBehavior).Handle(entityContent, createUserAction);

            if (!userNewState.Ok) return userNewState;

            //var addEmployeeToRoleAction = new AddEmployeeAction(
            //    addEmployeeBusinessProcessAction.RoleId,
            //    ((Employee)employeeNewState.Value.FirstOrDefault()).Id,
            //    addEmployeeBusinessProcessAction.FullName,
            //    addEmployeeBusinessProcessAction.Position
            //);

            //var roleNewState = new NextState(addEmployeeToRoleBehavior).Handle(entityContent, addEmployeeToRoleAction);

            //if (!roleNewState.Ok) return userNewState;

            return new Result<IEntity[]>(employeeNewState.Value.Concat(userNewState.Value)/*.Concat(roleNewState.Value)*/.ToArray());
        }
    }



    public class CreateEmployeeBusinessProcessAction : Common.Action
    {
        public CreateEmployeeBusinessProcessAction(
            string fullName,
            string position,
            string phoneNumber,
            string email,
            DateTime dateOfBirth,
            string personalId,
            JobType jobType,
            int roleId)
        {
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            PersonalId = personalId;
            JobType = jobType;
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
