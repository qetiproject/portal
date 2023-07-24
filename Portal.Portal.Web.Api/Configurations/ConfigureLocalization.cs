using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureLocalization : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ka-GE"),
                SupportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ka-GE") },
                SupportedUICultures = new[] { new CultureInfo("en-US"), new CultureInfo("ka-GE") },
                RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() }
            });

            return app;
        }
    }
}
