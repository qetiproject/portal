using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;

namespace Portal.Portal.Application.EmployeeServiceModule.EditEmployeeUniversityFeature
{
    public class EditEmployeeUniversityBehavior : Behavior<Employee, EditEmployeeUniversityAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditEmployeeUniversityAction action)
        {
            var university = rootEntity.Universities.FirstOrDefault(u => u.Id == action.UniversityId);
            if (university is not null)
            {
                university.University = action.University;
                university.Faculty = action.Faculty;
                university.StartDate = action.StartDate;
                university.EndDate = action.EndDate;
                university.Certificate = action.File != null
                        ? new EmployeeFile(action.File.FileName, action.File.FilePath, action.File.Type)
                        : new EmployeeFile();
            }

            return new Result<Employee>(rootEntity);
        }
    }
}
