using Portal.Portal.Application.NonComplianceModule.ChangeNonComplianceStatusFeature;
using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.NonComplianceModule.AddNonComplianceCommentFeature
{
    public class AddNonComplianceCommentBehavior : Behavior<NonCompliance, AddNonComplianceCommentAction>
    {
        public override Result<NonCompliance> Behave(NonCompliance rootEntity, AddNonComplianceCommentAction action)
        {
            rootEntity.ViolatorComment = action.Comment;
            rootEntity.ViolatorFile = new NonComplianceFile(action.File?.FileName, action.File?.FilePath, action.File?.Type);

            return new Result<NonCompliance>(rootEntity);
        }
    }
}
