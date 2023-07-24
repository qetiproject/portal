using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.ApplicationUsers.Models;

namespace Portal.Portal.Persistence
{
    public class CustomUserStore : UserStore<User, Role, PortalDbContext, int>
    {
        public CustomUserStore(PortalDbContext context)
            : base(context)
        {
            AutoSaveChanges = false;
        }
    }
}
