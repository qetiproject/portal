using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditFormerPositionFeature
{
    public class EditFormerPositionActionBusinessRules : AbstractValidator<EditFormerPositionAction>
    {
        public EditFormerPositionActionBusinessRules()
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
