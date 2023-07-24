using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Common;

namespace Portal.Portal.Application.RolesModule.Models.ChildModels
{
    public record RolePermission : Record
    {
        public RolePermission()
        {
            
        }

        public RolePermission(
            int permissionId,
            string permissionKey,
            string description)
        {
            PermissionId = permissionId;
            PermissionKey = permissionKey;
            Description = description;
        }

        public int PermissionId { get; set; }

        public string PermissionKey { get; set; }

        public string Description { get; set; }
    }
}
