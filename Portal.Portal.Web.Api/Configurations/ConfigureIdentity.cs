using Microsoft.AspNetCore.Identity;
using Portal.Portal.Application.ApplicationRoles.Models;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Persistence;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureIdentity : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserStore<User>, CustomUserStore>();

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<PortalDbContext>()
            .AddDefaultTokenProviders();

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            return app;
        }
    }
}
