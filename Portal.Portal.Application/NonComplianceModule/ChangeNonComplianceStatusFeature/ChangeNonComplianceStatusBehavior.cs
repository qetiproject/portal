using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;

namespace Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature
{
    public class ChangeNonComplianceStatusBehavior : Behavior<NonCompliance, ChangeNonComplianceStatusAction>
    {
        public override Result<NonCompliance> Behave(NonCompliance rootEntity, ChangeNonComplianceStatusAction action)
        {
            rootEntity.StatusChanger = action.StatusChanger;
            rootEntity.Status = action.Status;
            rootEntity.StatusComment = action.Comment;
            rootEntity.StatusChangeDate = DateTime.Now;
            rootEntity.StatusFile = new NonComplianceFile(action.File?.FileName, action.File?.FilePath, action.File?.Type);

            return new Result<NonCompliance>(rootEntity);
        }
    }
}
