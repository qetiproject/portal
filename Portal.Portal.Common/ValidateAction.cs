using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Common
{
    public class ValidateAction
    {
        public Action Action { get; }

        public string[] Messages { get; }

        public ValidateAction(Action action)
        {
            Action = action;
        }

        public ValidationResult Validate()
        {
            var typeOfValidator = (Validate?)Action.GetType().GetCustomAttribute(typeof(Validate));

            if (typeOfValidator != null)
            {
                var validator = (dynamic)Activator.CreateInstance(typeOfValidator.Validator);

                return (ValidationResult)validator.Validate((dynamic?)Action);
            }
            else
            {
                return new ValidationResult();
            }
        }
    }
}
