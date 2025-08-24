using Microsoft.EntityFrameworkCore;
using Ticketing.Data;
using Ticketing.Data.Repositories;
using Ticketing.Data.Repositories.Interfaces;
using Ticketing.Services;
using Ticketing.Services.Interfaces;

namespace Ticketing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTicketManagement(this IServiceCollection services)
    {

        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }

    public static IServiceCollection AddTicketDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TicketDbContext>(options =>
                options.UseInMemoryDatabase("TicketDb"));

        return services;
    }


}