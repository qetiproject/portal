using FluentValidation;
using Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature
{
    public class ChangeNonComplianceStatusActionBusinessRules : AbstractValidator<ChangeNonComplianceStatusAction>
    {
        public ChangeNonComplianceStatusActionBusinessRules()
        {
            RuleFor(Action => Action.Status)
                .NotNull()
                .NotEmpty();
        }
    }
}
