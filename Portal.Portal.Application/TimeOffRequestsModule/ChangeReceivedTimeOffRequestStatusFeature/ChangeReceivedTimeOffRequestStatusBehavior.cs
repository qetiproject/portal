using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature
{
    public class ChangeReceivedTimeOffRequestStatusBehavior : Behavior<TimeOffRequest, ChangeReceivedTimeOffRequestStatusAction>
    {
        public override Result<TimeOffRequest> Behave(TimeOffRequest rootEntity, ChangeReceivedTimeOffRequestStatusAction action)
        {
            rootEntity.StatusChanger = action.StatusChanger;
            rootEntity.Status = action.Status;
            rootEntity.StatusComment = action.Comment;
            rootEntity.StatusChangeDate = DateTime.Now;
            rootEntity.StatusFile = new TimeOffRequestFile(action.File?.FileName, action.File?.FilePath, action.File?.Type);

            return new Result<TimeOffRequest>(rootEntity);
        }
    }
}
