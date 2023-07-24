using FluentValidation;
using Portal.Portal.Application.RolesModule.CreateRoleFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.EditRoleFeature
{
    public class EditRoleActionBusinessRules : AbstractValidator<EditRoleAction>
    {
        public EditRoleActionBusinessRules()
        {
            RuleFor(Action => Action.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
