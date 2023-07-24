using Microsoft.EntityFrameworkCore;
using Portal.Portal.Persistence;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureDbContext : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("PortalConnectionString").Value;

            builder.Services.AddDbContext<PortalDbContext>(
                options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(connectionString);
                }
            );

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            return app;
        }
    }
}
