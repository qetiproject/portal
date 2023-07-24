using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature
{
    public class AddComputerSkillBehavior : Behavior<Employee, AddComputerSkillAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, AddComputerSkillAction action)
        {
            rootEntity.ComputerSkills.Add(new ComputerSkill(action.Skill));

            return new Result<Employee>(rootEntity);
        }
    }
}
