using Portal.Portal.Application.EmployeeServiceModule.DeleteComputerSkillFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteLanguageFeature
{
    public class DeleteLanguageBehavior : Behavior<Employee, DeleteLanguageAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, DeleteLanguageAction action)
        {
            var language = rootEntity.Languages.FirstOrDefault(x => x.Id == action.LanguageId);

            if (language is not null)
                rootEntity.Languages.Remove(language);

            return new Result<Employee>(rootEntity);
        }
    }
}
