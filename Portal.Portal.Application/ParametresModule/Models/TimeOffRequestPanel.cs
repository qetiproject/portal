using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portal.Portal.Application.AdminPanelModule.Models
{
    public record TimeOffRequestPanel : Record, IEntity
    {
        public TimeOffRequestPanel()
        {
        }

        public string? HRWhoReceivesTimeOffRequests { get; set; }
    }
}
