using Microsoft.EntityFrameworkCore;
using Portal.Portal.Common;
using Portal.Portal.Persistence;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureService : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddScoped<IPhotoUploadService, PhotoUploadService>();
            builder.Services.AddScoped<IFileUploadService, FileUploadService>();

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            return app;
        }
    }
}
