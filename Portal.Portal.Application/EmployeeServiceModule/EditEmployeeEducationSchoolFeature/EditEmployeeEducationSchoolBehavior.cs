using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeEducationSchoolFeature
{
    public class EditEmployeeEducationSchoolBehavior : Behavior<Employee, EditEmployeeEducationSchoolAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeeEducationSchoolAction action)
        {
            //rootEntity.Education.School = action.School;

            return new Result<Employee>(rootEntity);
        }
    }
}
