using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.RedirectTimeOffRequestFeature
{
    public class RedirecTimeOffRequestBehavior : Behavior<TimeOffRequest, RedirecTimeOffRequestAction>
    {
        public override Result<TimeOffRequest> Behave(TimeOffRequest rootEntity, RedirecTimeOffRequestAction action)
        {
            rootEntity.Redirects.Add(new Redirect(
                action.Redirecter,
                action.Participant,
                action.Comment,
                action.RightOfConfirmation,
                new TimeOffRequestFile(action.File?.FileName, action.File?.FilePath, action.File?.Type)));

            return new Result<TimeOffRequest>(rootEntity);
        }
    }
}
