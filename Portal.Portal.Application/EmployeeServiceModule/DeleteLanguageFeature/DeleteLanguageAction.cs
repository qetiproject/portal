using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.DeleteLanguageFeature
{
    public class DeleteLanguageAction : Common.Action
    {
        public DeleteLanguageAction(int id, int languageId)
        {
            Id = id;
            LanguageId = languageId;
        }

        public int LanguageId { get; set; }
    }
}
