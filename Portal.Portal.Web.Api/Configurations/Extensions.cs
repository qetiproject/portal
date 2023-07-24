using Portal.Portal.Persistence;

namespace Portal.Portal.Web.Api.Configurations
{
    public static class Extensions
    {
        public static WebApplicationBuilder Apply(this WebApplicationBuilder builder, IEnumerable<IConfiguration> configurations)
        {
            return configurations.Aggregate(builder, (p, n) => n.ConfigureServices(p));
        }

        public static WebApplication Apply(this WebApplication app, IEnumerable<IConfiguration> configurations)
        {
            return configurations.Reverse().Aggregate(app, (p, n) => n.ConfigurePipelines(p));
        }
    }
}
