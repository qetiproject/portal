using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.EmployeeServiceModule.Models;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeUniversityFeature
{
    public class DeleteEmployeeUniversityBehavior : Behavior<Employee, DeleteEmployeeUniversityAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteEmployeeUniversityAction action)
        {
            var university = rootEntity.Universities.FirstOrDefault(u => u.Id == action.UniversityId);

            if (university is not null)
                rootEntity.Universities.Remove(university);

            return new Result<Employee>(rootEntity);
        }
    }
}
