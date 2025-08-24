using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Interfaces;

public interface IModule
{
    void RegisterServices(IServiceCollection services, IConfiguration configuration);
    void WebAppConfiguration(WebApplication endpoints);
}

