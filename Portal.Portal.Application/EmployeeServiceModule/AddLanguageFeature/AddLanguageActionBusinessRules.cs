using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddLanguageFeature
{
    public class AddLanguageActionBusinessRules : AbstractValidator<AddLanguageAction>
    {
        public AddLanguageActionBusinessRules()
        {
            RuleFor(Action => Action.Language)
                .NotNull()
                .NotEmpty();
        }
    }
}
