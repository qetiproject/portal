using Portal.Portal.Application.AdminPanelModule.CreateGroupFeature;
using Portal.Portal.Application.AdminPanelModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.AdminPanelModule.UpdateTimeOffRequestPanelFeature
{
    public class UpdateTimeOffRequestPanelBehavior : Behavior<TimeOffRequestPanel, UpdateTimeOffRequestPanelAction>
    {
        public override Result<TimeOffRequestPanel> Behave(TimeOffRequestPanel rootEntity, UpdateTimeOffRequestPanelAction action)
        {
            if (rootEntity is null)
            {
                rootEntity = new TimeOffRequestPanel();
            }

            rootEntity.HRWhoReceivesTimeOffRequests = action.HRWhoReceivesTimeOffRequests;

            return new Result<TimeOffRequestPanel>(rootEntity);
        }
    }
}
