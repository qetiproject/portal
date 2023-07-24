using Portal.Portal.Web.Api.Shared;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureAutoMigration : IConfiguration
    {
        ConfigurationManager configuration;
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            configuration = builder.Configuration;

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            AutoMigration.Initialize(app, configuration);

            return app;
        }
    }
}
