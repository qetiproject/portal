using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditComputerSkillFeature
{
    public class EditComputerSkillBehavior : Behavior<Employee, EditComputerSkillAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditComputerSkillAction action)
        {
            var skill = rootEntity.ComputerSkills.FirstOrDefault(s => s.Id == action.SkillId);
            if (skill is not null)
                skill.Skill = action.Skill;

            return new Result<Employee>(rootEntity);
        }
    }
}
