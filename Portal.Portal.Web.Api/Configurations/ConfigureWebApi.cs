using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

namespace Portal.Portal.Web.Api.Configurations
{
    public class ConfigureWebApi : IConfiguration
    {
        public WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddCors();

            builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
                {
                    //options.SuppressConsumesConstraintForFormFileParameters = true;
                    //options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                });

            return builder;
        }

        public WebApplication ConfigurePipelines(WebApplication app)
        {
            app.UseCors(conf =>
            {
                conf.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            
            string photosPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Photos");
            string filesPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Files");
            string draftsPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Drafts");

            if (!Directory.Exists(photosPath))
            {
                Directory.CreateDirectory(photosPath);
            }

            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            if (!Directory.Exists(draftsPath))
            {
                Directory.CreateDirectory(draftsPath);
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(photosPath),
                RequestPath = new PathString("/App_Data/Photos")
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(filesPath),
                RequestPath = new PathString("/App_Data/Files")
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(draftsPath),
                RequestPath = new PathString("/App_Data/Drafts")
            });

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
