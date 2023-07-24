using FluentValidation;
using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.CreateRoleFeature
{
    public class CreateRoleActionBusinessRules : AbstractValidator<CreateRoleAction>
    {
        public CreateRoleActionBusinessRules()
        {
            RuleFor(Action => Action.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
