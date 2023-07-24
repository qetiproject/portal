using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.AdminPanelModule.Models;

namespace Portal.Portal.Application.AdminPanelModule.CreateGroupFeature
{
    public class CreateGroupBehavior : Behavior<Group, CreateGroupAction>
    {
        public override Result<Group> Behave(Group rootEntity, CreateGroupAction action)
        {
            var group = new Group(action.Group);

            return new Result<Group>(group);
        }
    }
}
