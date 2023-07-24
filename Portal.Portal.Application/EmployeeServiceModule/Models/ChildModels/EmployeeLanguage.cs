using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels
{
    public record EmployeeLanguage : Record
    {
        public EmployeeLanguage()
        {

        }

        public EmployeeLanguage(string language)
        {
            Language = language;
        }

        public string Language { get; set; }
    }
}
