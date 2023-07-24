using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portal.Portal.Application.ApplicationRoles.Models
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role()
        {
        }

        public Role(string role, string description) : base(role)
        {
            base.Name = role;
            Description = description;
            CreateDate = DateTime.UtcNow.AddHours(4);
            Permissions = new HashSet<RolePermission>();
            Users = new HashSet<RoleUser>();
        }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public HashSet<RolePermission> Permissions { get; set; }

        public HashSet<RoleUser> Users { get; set; }
    }
}
