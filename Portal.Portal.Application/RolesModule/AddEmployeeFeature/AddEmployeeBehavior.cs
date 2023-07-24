using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.RolesModule.EditRoleFeature;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.AddEmployeeFeature
{
    public class AddEmployeeBehavior : Behavior<Role, AddEmployeeAction>
    {
        public override Result<Role> Behave(Role rootEntity, AddEmployeeAction action)
        {
            rootEntity.Users.Add(
                new RoleUser(
                    action.UserId, 
                    action.FullName,
                    action.Position
                    )
                );

            return new Result<Role>(rootEntity);
        }
    }
}
