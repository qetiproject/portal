using Portal.Portal.Application.RolesModule.EditRoleFeature;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.AddEmployeeFeature
{
    public class AddEmployeeAction : Common.Action
    {
        public AddEmployeeAction(
            int id,
            int userId,
            string fullName,
            string position)
        {
            Id = id;
            UserId = userId;
            FullName = fullName;
            Position = position;
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}
