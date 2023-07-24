using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.CreateContractTypeFeature
{
    public class CreateContractTypeActionBusinessRules : AbstractValidator<CreateContractTypeAction>
    {
        public CreateContractTypeActionBusinessRules()
        {
            RuleFor(Action => Action.ContractType)
                .NotNull()
                .NotEmpty();
        }
    }
}
