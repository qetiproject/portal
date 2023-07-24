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
    [Validate(typeof(EditRoleActionBusinessRules))]
    public class EditRoleAction : Common.Action
    {
        public EditRoleAction(
            int id,
            string name,
            string? description,
            List<Permission> permissions)
        {
            Id = id;
            Name = name;
            Permissions = permissions;
            Description = description;
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
