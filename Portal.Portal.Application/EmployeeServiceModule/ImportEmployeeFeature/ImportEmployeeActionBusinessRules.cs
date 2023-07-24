using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.ImportEmployeeFeature
{
    public class ImportEmployeeActionBusinessRules : AbstractValidator<ImportEmployeeAction>
    {
        public ImportEmployeeActionBusinessRules()
        {
            RuleFor(Action => Action.FullName)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.Position)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.PhoneNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.DateOfBirth)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(Action => Action.PersonalId)
                .NotNull()
                .NotEmpty()
                .Length(11);
        }
    }
}
