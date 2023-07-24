using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.UpdateTimeOffRequestPanelFeature
{
    public class UpdateTimeOffRequestPanelAction : Common.Action
    {
        public UpdateTimeOffRequestPanelAction(int? id, string? hRWhoReceivesTimeOffRequests)
        {
            Id = id;
            HRWhoReceivesTimeOffRequests = hRWhoReceivesTimeOffRequests;
        }

        public string? HRWhoReceivesTimeOffRequests { get; set; }
    }
}
