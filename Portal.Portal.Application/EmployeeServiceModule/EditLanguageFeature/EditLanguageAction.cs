using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.EditLanguageFeature
{
    public class EditLanguageAction : Common.Action
    {
        public EditLanguageAction(int id, int languageId, string language)
        {
            Id = id;
            LanguageId = languageId;
            Language = language;
        }

        public int LanguageId { get; set; }

        public string Language { get; set; }
    }
}
