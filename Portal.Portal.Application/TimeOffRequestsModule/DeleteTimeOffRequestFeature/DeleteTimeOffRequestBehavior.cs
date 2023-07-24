using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.DeleteTimeOffRequestFeature
{
    public class DeleteTimeOffRequestBehavior : Behavior<TimeOffRequest, DeleteTimeOffRequestAction>
    {
        public override Result<TimeOffRequest> Behave(TimeOffRequest rootEntity, DeleteTimeOffRequestAction action)
        {
            return new Result<TimeOffRequest>(rootEntity);
        }
    }
}
