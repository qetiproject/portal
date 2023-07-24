using Portal.Portal.Application.NonComplianceModule.CreateNonComplianceFeature;
using Portal.Portal.Application.RolesModule.Models.ChildModels;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.CreateRoleFeature
{
    [Validate(typeof(CreateRoleActionBusinessRules))]
    public class CreateRoleAction : Common.Action
    {
        public CreateRoleAction(
            string name,
            string? description,
            List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Description = description;
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
