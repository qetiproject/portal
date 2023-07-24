using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.ApplicationUsers.Models;
using System.Configuration;
using Portal.Portal.Persistence;

namespace Portal.Portal.Web.Api.Shared
{
    public static class AutoMigration
    {
        public static void Initialize(IApplicationBuilder app, IConfiguration configuration)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceProvider>().CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<PortalDbContext>())
                {
                    db.Database.Migrate();
                    Seed.Initialize(db, configuration, scope.ServiceProvider.GetRequiredService<UserManager<User>>(), scope.ServiceProvider.GetRequiredService<RoleManager<Role>>());
                }
            }
        }
    }
}
