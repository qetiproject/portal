using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.RolesModule.AddEmployeeFeature;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.DeleteEmployeeFeature
{
    public class DeleteEmployeeBehavior : Behavior<Role, DeleteEmployeeAction>
    {
        public override Result<Role> Behave(Role rootEntity, DeleteEmployeeAction action)
        {
            var user = rootEntity.Users.FirstOrDefault(u => u.UserId == action.UserId);

            rootEntity.Users.Remove(user);

            return new Result<Role>(rootEntity);
        }
    }
}
