using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.Models.ChildModels
{
    public record PermissionGroup : Record, IEntity
    {
        public PermissionGroup()
        {

        }

        public PermissionGroup(string name)
        {
            Name = name;
            Permissions = new HashSet<Permission>();
        }

        public string Name { get; set; }

        public HashSet<Permission> Permissions { get; set; }
    }
}
