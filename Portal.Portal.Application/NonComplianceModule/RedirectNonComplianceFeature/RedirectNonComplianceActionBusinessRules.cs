using FluentValidation;
using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature
{
    public class RedirectNonComplianceActionBusinessRules : AbstractValidator<RedirectNonComplianceAction>
    {
        public RedirectNonComplianceActionBusinessRules()
        {
            RuleFor(Action => Action.Receiver)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.StatusChange)
                .NotNull();
        }
    }
}
