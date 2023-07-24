using FluentValidation;
using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.CreateGroupFeature
{
    public class CreateGroupActionBusinessRules : AbstractValidator<CreateGroupAction>
    {
        public CreateGroupActionBusinessRules()
        {
            RuleFor(Action => Action.Group)
                .NotNull()
                .NotEmpty();
        }
    }
}
