using Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature
{
    [Validate(typeof(AddComputerSkillActionBusinessRules))]
    public class AddComputerSkillAction : Common.Action
    {
        public AddComputerSkillAction(int id, string skill)
        {
            Id = id;
            Skill = skill;
        }

        public string Skill { get; set; }
    }
}
