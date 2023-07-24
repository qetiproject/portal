using System;

namespace Portal.Portal.Common
{
    public class Validate : Attribute
    {
        public Type Validator { get; set; }

        public Validate(Type validator)
        {
            Validator = validator;
        }
    }
}
