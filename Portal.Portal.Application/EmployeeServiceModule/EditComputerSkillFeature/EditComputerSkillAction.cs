using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditComputerSkillFeature
{
    public class EditComputerSkillAction : Common.Action
    {
        public EditComputerSkillAction(int id, int skillId, string skill)
        {
            Id = id;
            SkillId = skillId;
            Skill = skill;
        }

        public int SkillId { get; set; }

        public string Skill { get; set; }
    }
}
