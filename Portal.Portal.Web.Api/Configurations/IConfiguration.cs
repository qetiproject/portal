namespace Portal.Portal.Web.Api.Configurations
{
    public interface IConfiguration
    {
        WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder);
        WebApplication ConfigurePipelines(WebApplication app);
    }
}
