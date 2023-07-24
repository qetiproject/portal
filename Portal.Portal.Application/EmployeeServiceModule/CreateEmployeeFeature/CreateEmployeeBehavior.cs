using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature
{
    public class CreateEmployeeBehavior : Behavior<Employee, CreateEmployeeAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, CreateEmployeeAction action)
        {
            var personalInformation = new PersonalInformation(
                action.FullName,
                action.Position,
                action.PhoneNumber,
                action.Email,
                action.DateOfBirth,
                action.PersonalId,
                action.JobType);

            var employee = new Employee(personalInformation);

            employee.EmploymentHistory ??= new EmploymentHistory();

            employee.EmploymentHistory.EmployeeRoleId = action.RoleId;

            return new Result<Employee>(employee);
        }
    }
}

