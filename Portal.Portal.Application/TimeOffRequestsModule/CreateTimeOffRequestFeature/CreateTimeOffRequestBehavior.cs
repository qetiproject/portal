using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.TimeOffRequestsModule.Models;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;

namespace Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature
{
    public class CreateTimeOffRequestBehavior : Behavior<TimeOffRequest, CreateTimeOffRequestAction>
    {
        public override Result<TimeOffRequest> Behave(TimeOffRequest rootEntity, CreateTimeOffRequestAction action)
        {
            var title = action.Title;
            if(action.Type != Models.Enums.TimeOffRequestType.Custom)
            {
                title = action.Type.ToString();
            }

            var timeOffRequest = new TimeOffRequest(
                action.Type,
                title,
                action.Sender,
                action.Receiver,
                action.DeadLine,
                action.From,
                action.Including,
                action.Description,
                new TimeOffRequestFile(action.File?.FileName, action.File?.FilePath, action.File?.Type));

            return new Result<TimeOffRequest>(timeOffRequest);
        }
    }
}
