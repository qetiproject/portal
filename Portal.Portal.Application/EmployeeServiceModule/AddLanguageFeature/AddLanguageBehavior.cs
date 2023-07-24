using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddLanguageFeature
{
    public class AddLanguageBehavior : Behavior<Employee, AddLanguageAction>
    {
        public override Result<Employee> Behave(Employee rootEntity, AddLanguageAction action)
        {
            rootEntity.Languages.Add(new EmployeeLanguage(action.Language));

            return new Result<Employee>(rootEntity);
        }
    }
}
