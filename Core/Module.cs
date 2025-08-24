using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public abstract class Module : IModule
{
    protected abstract void ConfigModuleWebApplication(WebApplication app);

    public void WebAppConfiguration(WebApplication app)
    {
        ConfigModuleWebApplication(app);
    }

    public abstract void RegisterServices(IServiceCollection services, IConfiguration configuration);
}
