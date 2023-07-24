using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteComputerSkillFeature
{
    public class DeleteComputerSkillAction : Common.Action
    {
        public DeleteComputerSkillAction(int id, int skillId)
        {
            Id = id;
            SkillId = skillId;
        }

        public int SkillId { get; set; }
    }
}
