using Microsoft.Extensions.Configuration;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureEmail : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;

            //var emailConfig = configuration.GetSection("EmailConfiguration")
            //                    .Get<EmailConfiguration>(); 
            builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));

            //builder.Services.AddSingleton(emailConfig);

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            return app;
        }
    }

    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
