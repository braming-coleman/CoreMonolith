using CoreMonolith.Application.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.DownloadService.Api.Usenet;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Infrastructure.Clients.SabNzbd;
using Modules.DownloadService.Infrastructure.Services;

namespace Modules.DownloadService.Infrastructure;

public class ModuleServicesInstaller : IModuleServicesInstaller
{
    public void ApplyMigrations(IApplicationBuilder app)
    {
        //No migrations yet.
    }

    public void InstallDatabase(IHostApplicationBuilder builder)
    {
        //No dbs yet.
    }

    public void InstallServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ISabNzbdServiceApi, SabNzbdServiceApi>();
        services.AddScoped<ISabNzbdClient, SabNzbdClient>();
    }
}
