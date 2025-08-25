using Core;
using Application;
using Infrastructure;
using ToDo.Api.Extensions;
namespace ToDo.Api;

public class TicketModule : Module
{
    public override void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddPresentation()
            .AddInfrastructure(configuration);

    }

    protected override void ConfigModuleWebApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();
        }

        app.UseRequestContextLogging();

        app.UseExceptionHandler();

        app.UseAuthentication();

        app.UseAuthorization();
    }
}
