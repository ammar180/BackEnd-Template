using Core;
using Ticketing.Extensions;

namespace Ticketing.Api;

public class TicketModule : Module
{
    public override void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTicketManagement();
        services.AddTicketDbContext();
    }

    protected override void ConfigModuleWebApplication(WebApplication app)
    {
    }
}
