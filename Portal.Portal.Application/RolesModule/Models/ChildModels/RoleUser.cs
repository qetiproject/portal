using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.RolesModule.Models.ChildModels
{
    public record RoleUser : Record
    {
        public RoleUser()
        {

        }

        public RoleUser(
            int userId,
            string fullName,
            string position)
        {
            UserId = userId;
            FullName = fullName;
            Position = position;
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}
