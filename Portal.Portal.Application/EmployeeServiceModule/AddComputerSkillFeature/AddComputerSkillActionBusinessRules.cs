using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature
{
    public class AddComputerSkillActionBusinessRules : AbstractValidator<AddComputerSkillAction>
    {
        public AddComputerSkillActionBusinessRules()
        {
            RuleFor(Action => Action.Skill)
                .NotNull()
                .NotEmpty();
        }
    }
}
