using Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;

namespace Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature
{
    public class CreateNonComplianceBehavior : Behavior<NonCompliance, CreateNonComplianceAction>
    {
        public override Result<NonCompliance> Behave(NonCompliance rootEntity, CreateNonComplianceAction action)
        {
            var nonCompliance = new NonCompliance(
                action.Title,
                action.Sender,
                action.Violator,
                action.ApprovalDeadline,
                action.Fine,
                action.Description,
                new NonComplianceFile(action.File?.FileName, action.File?.FilePath, action.File?.Type));

            return new Result<NonCompliance>(nonCompliance);
        }
    }
}
