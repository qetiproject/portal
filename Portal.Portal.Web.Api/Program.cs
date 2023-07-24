using Portal.Portal.Web.Api.Configurations;
using IConfiguration = Portal.Portal.Web.Api.Configurations.IConfiguration;

var configurations = new List<IConfiguration>()
            {
                new ConfigureLocalization(),
                new ConfigureDbContext(),
                new ConfigureIdentity(),
                new ConfigureAuthentication(),
                new ConfigureWebApi(),
                new ConfigureSwagger(),
                new ConfigureEmail(),
                new ConfigureAutoMigration(),
                new ConfigureService()
            };

WebApplication
    .CreateBuilder(args)
    .Apply(configurations)
    .Build()
    .Apply(configurations)
    .Run();