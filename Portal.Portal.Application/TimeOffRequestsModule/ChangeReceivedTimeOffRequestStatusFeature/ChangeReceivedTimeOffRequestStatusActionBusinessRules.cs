using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature
{
    public class ChangeReceivedTimeOffRequestStatusActionBusinessRules : AbstractValidator<ChangeReceivedTimeOffRequestStatusAction>
    {
        public ChangeReceivedTimeOffRequestStatusActionBusinessRules()
        {
            RuleFor(Action => Action.Status)
                .NotNull()
                .NotEmpty();
        }
    }
}
