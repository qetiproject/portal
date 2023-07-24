using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Portal.Application.RolesModule.Models.ChildModels
{
    public record Permission : Record
    {
        public Permission()
        {

        }

        public Permission(
            string permissionKey,
            string description)
        {
            PermissionKey = permissionKey;
            Description = description;
        }

        public string PermissionKey { get; set; }

        public string Description { get; set; }
    }
}
