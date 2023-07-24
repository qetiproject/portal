using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.CreateGroupFeature
{
    [Validate(typeof(CreateGroupActionBusinessRules))]
    public class CreateGroupAction : Common.Action
    {
        public CreateGroupAction(string group)
        {
            Group = group;
        }

        public string Group { get; set; }
    }
}
