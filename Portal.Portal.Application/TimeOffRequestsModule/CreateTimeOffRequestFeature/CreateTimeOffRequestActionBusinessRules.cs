using FluentValidation;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature
{
    internal class CreateTimeOffRequestActionBusinessRules : AbstractValidator<CreateTimeOffRequestAction>
    {
        public CreateTimeOffRequestActionBusinessRules()
        {
            RuleFor(Action => Action.Title)
                .NotNull()
                .NotEmpty()
                .When(action => action.Type == Models.Enums.TimeOffRequestType.Custom);
            RuleFor(Action => Action.Receiver)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.From)
                .NotNull()
                .NotEmpty()
                .When(action => action.Type != Models.Enums.TimeOffRequestType.Custom);
            RuleFor(Action => Action.Including)
                .NotNull()
                .NotEmpty()
                .GreaterThan(action => action.From)
                .When(action => action.Type == Models.Enums.TimeOffRequestType.Vacation 
                || action.Type == Models.Enums.TimeOffRequestType.BusinessTrip 
                || action.Type == Models.Enums.TimeOffRequestType.UnpaidVacation);
        }
    }
}
