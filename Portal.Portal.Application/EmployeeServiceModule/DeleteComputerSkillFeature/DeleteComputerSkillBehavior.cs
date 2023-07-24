using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteComputerSkillFeature
{
    public class DeleteComputerSkillBehavior : Behavior<Employee, DeleteComputerSkillAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteComputerSkillAction action)
        {
            var skill = rootEntity.ComputerSkills.FirstOrDefault(x => x.Id == action.SkillId);

            if (skill is not null)
                rootEntity.ComputerSkills.Remove(skill);

            return new Result<Employee>(rootEntity);
        }
    }
}
