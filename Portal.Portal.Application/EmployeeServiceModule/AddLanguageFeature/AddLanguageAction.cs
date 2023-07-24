using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddLanguageFeature
{
    [Validate(typeof(AddLanguageActionBusinessRules))]
    public class AddLanguageAction : Common.Action
    {
        public AddLanguageAction(int id, string language)
        {
            Id = id;
            Language = language;
        }

        public string Language { get; set; }
    }
}
