using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.RolesModule.CreateRoleFeature;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.EditRoleFeature
{
    public class EditRoleBehavior : Behavior<Role, EditRoleAction>
    {
        public override Result<Role> Behave(Role rootEntity, EditRoleAction action)
        {
            rootEntity.Name = action.Name;
            rootEntity.Description = action.Description;
            rootEntity.Permissions.Clear();

            foreach (var permission in action.Permissions)
            {
                rootEntity.Permissions.Add(new RolePermission(
                    permission.Id,
                    permission.PermissionKey,
                    permission.Description
                ));
            }

            return new Result<Role>(rootEntity);
        }
    }
}
