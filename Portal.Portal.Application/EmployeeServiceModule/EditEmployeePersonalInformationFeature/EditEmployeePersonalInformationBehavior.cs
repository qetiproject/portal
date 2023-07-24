using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeePersonalInformationFeature
{
    public class EditEmployeePersonalInformationBehavior : Behavior<Employee, EditEmployeePersonalInformationAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeePersonalInformationAction action)
        {
            var personalInformation = rootEntity.PersonalInformation;
            personalInformation.FullName = action.FullName;
            personalInformation.Position = action.Position;
            personalInformation.PhoneNumber = action.PhoneNumber;
            personalInformation.Email = action.Email;
            personalInformation.DateOfBirth = action.DateOfBirth;
            personalInformation.PersonalId = action.PersonalId;
            personalInformation.JobType = action.JobType;
            personalInformation.Status = action.Status;

            //var personalInformation = new PersonalInformation(
            //    action.FullName,
            //    action.Position,
            //    action.PhoneNumber,
            //    action.Email,
            //    action.DateOfBirth,
            //    action.PersonalId,
            //    action.JobType,
            //    action.Status);

            //rootEntity = rootEntity with { PersonalInformation = personalInformation };

            return new Result<Employee>(rootEntity);
        }
    }
}
