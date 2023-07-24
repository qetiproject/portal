using Microsoft.AspNetCore.Identity;
using Portal.Portal.Common;
using Portal.Portal.Application.ApplicationRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.ApplicationUsers.Models
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            CreateDate = DateTime.UtcNow.AddHours(4);
        }

        public DateTime CreateDate { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
}
