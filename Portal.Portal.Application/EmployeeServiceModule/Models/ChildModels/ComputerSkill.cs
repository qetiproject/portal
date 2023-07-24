using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record ComputerSkill : Record
    {
        public ComputerSkill()
        {

        }

        public ComputerSkill(string skill)
        {
            Skill = skill;
        }

        public string Skill { get; set; }
    }
}
