using FluentValidation;
using Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.AddNonComplianceCommentFeature
{
    public class AddNonComplianceCommentActionBusinessRules : AbstractValidator<AddNonComplianceCommentAction>
    {
        public AddNonComplianceCommentActionBusinessRules()
        {
            RuleFor(Action => Action.Comment)
                .NotNull()
                .NotEmpty();
        }
    }
}
