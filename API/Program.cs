using API.Extensions;
using Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IEnumerable<Module> modules = LoadPluginModules(builder);

foreach (var module in modules)
{
    module.RegisterServices(builder.Services, builder.Configuration);

    var moduleAssembly = module.GetType().Assembly;
    builder.Services.AddControllers()
        .PartManager.ApplicationParts.Add(new AssemblyPart(moduleAssembly));
}

builder.Services.AddSwaggerGenWithAuth();

var app = builder.Build();

foreach (var module in modules)
{
    module.WebAppConfiguration(app);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

static IEnumerable<Module> LoadPluginModules(WebApplicationBuilder builder)
{
    string pluginsFolder = builder.Configuration["PluginsFolder"];

    if (string.IsNullOrEmpty(pluginsFolder))
    {
        pluginsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "build-dev");
    }

    Console.WriteLine($"Loading plugins from: {pluginsFolder}");

    var modules = ModuleLoader.LoadModules(pluginsFolder);
    return modules;
}