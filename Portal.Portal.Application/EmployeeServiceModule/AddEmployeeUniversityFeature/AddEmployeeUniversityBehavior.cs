using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeEducationSchoolFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature
{
    public class AddEmployeeUniversityBehavior : Behavior<Employee, AddEmployeeUniversityAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, AddEmployeeUniversityAction action)
        {
            var file = action.File != null
                    ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                    : new EmployeeFile();

            var university = new EmployeeUniversity(
                action.University,
                action.Faculty,
                action.StartDate,
                action.EndDate,
                file
                );

            rootEntity.Universities.Add(university);

            return new Result<Employee>(rootEntity);
        }
    }
}
