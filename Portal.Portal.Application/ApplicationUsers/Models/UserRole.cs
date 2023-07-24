using Microsoft.AspNetCore.Identity;

namespace Portal.Portal.Application.ApplicationUsers.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public override int UserId { get => base.UserId; set => base.UserId = value; }

        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }
    }
}
