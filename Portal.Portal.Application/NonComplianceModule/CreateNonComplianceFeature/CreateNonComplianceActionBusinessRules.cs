using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature
{
    public class CreateNonComplianceActionBusinessRules : AbstractValidator<CreateNonComplianceAction>
    {
        public CreateNonComplianceActionBusinessRules()
        {
            RuleFor(Action => Action.Title)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.Violator)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.Description)
                .NotNull()
                .NotEmpty();
        }
    }
}
