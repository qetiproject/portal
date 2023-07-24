using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature
{
    public class AddFormerPositionActionBusinessRules : AbstractValidator<AddFormerPositionAction>
    {
        public AddFormerPositionActionBusinessRules()
        {
            RuleFor(Action => Action.Title)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.StartDate)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.EndDate)
                .NotNull()
                .NotEmpty();
        }
    }
}
