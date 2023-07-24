using FluentValidation;
using Portal.Portal.Application.EmployeeServiceModule.CreateEmployeeFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature
{
    public class AddEmployeeUniversityActionBusinessRules : AbstractValidator<AddEmployeeUniversityAction>
    {
        public AddEmployeeUniversityActionBusinessRules()
        {
            RuleFor(Action => Action.University)
                .NotNull()
                .NotEmpty();
            RuleFor(Action => Action.Faculty)
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
