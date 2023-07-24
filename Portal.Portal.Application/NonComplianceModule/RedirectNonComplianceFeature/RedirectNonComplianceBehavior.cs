using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;

namespace Portal.Portal.Application.NonComplianceModule.RedirectNonComplianceFeature
{
    public class RedirectNonComplianceBehavior : Behavior<NonCompliance, RedirectNonComplianceAction>
    {
        public override Result<NonCompliance> Behave(NonCompliance rootEntity, RedirectNonComplianceAction action)
        {
            rootEntity.Redirects.Add(new NonComplianceRedirect(
                action.Redirecter,
                action.Receiver,
                action.Comment,
                action.StatusChange,
                new NonComplianceFile(action.File?.FileName, action.File?.FilePath, action.File?.Type)));

            return new Result<NonCompliance>(rootEntity);
        }
    }
}
