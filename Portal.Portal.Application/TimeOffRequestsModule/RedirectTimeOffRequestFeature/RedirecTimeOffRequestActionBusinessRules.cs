using FluentValidation;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.RedirectTimeOffRequestFeature
{
    public class RedirecTimeOffRequestActionBusinessRules : AbstractValidator<RedirecTimeOffRequestAction>
    {
        public RedirecTimeOffRequestActionBusinessRules()
        {
            RuleFor(Action => Action.Participant)
                .NotNull()
                .NotEmpty();
        }
    }
}
