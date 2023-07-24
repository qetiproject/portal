using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using static System.Collections.Specialized.BitVector32;

namespace Portal.Portal.Application.EmployeeServiceModule.EditLanguageFeature
{
    public class EditLanguageBehavior : Behavior<Employee, EditLanguageAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, EditLanguageAction action)
        {
            var language = rootEntity.Languages.FirstOrDefault(l => l.Id == action.LanguageId);
            if (language is not null)
                language.Language = action.Language;

            return new Result<Employee>(rootEntity);
        }
    }
}
