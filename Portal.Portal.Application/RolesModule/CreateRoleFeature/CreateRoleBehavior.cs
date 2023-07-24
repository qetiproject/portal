using Portal.Portal.Application.NonComplianceModule.AddNonComplianceCommentFeature;
using Portal.Portal.Application.NonComplianceModule.Models.ChildModels;
using Portal.Portal.Application.NonComplianceModule.Models;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.RolesModule.Models.ChildModels;

namespace Portal.Portal.Application.RolesModule.CreateRoleFeature
{
    public class CreateRoleBehavior : Behavior<Role, CreateRoleAction>
    {
        public override Result<Role> Behave(Role rootEntity, CreateRoleAction action)
        {
            var role = new Role(action.Name, action.Description);

            foreach (var permission in action.Permissions)
            {
                role.Permissions.Add(new RolePermission(
                    permission.Id,
                    permission.PermissionKey,
                    permission.Description
                ));
            }

            return new Result<Role>(role);
        }
    }
}
