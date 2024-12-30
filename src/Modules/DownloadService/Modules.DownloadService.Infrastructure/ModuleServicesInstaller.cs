using CoreMonolith.Application;
using CoreMonolith.Application.Abstractions.Modules;
using CoreMonolith.ServiceDefaults.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.DownloadService.Api;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Application;
using Modules.DownloadService.Application.Abstractions.Data;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Infrastructure.Clients.SabNzbd;
using Modules.DownloadService.Infrastructure.Database;
using Modules.DownloadService.Infrastructure.Repositories;
using Modules.DownloadService.Infrastructure.Services;

namespace Modules.DownloadService.Infrastructure;

public class ModuleServicesInstaller : IModuleServicesInstaller
{
    public void ApplyMigrations(IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using DownloadServiceDbContext dbContext = scope.ServiceProvider.GetRequiredService<DownloadServiceDbContext>();

        dbContext.Database.Migrate();
    }

    public void InstallDatabase(IHostApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString(ConnectionNameConstants.DownloadServiceDbName)!;

        builder.Services.AddDbContext<DownloadServiceDbContext>(
            options => options
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default);
                    })
                .UseSnakeCaseNamingConvention());

        builder.Services.AddScoped<IDownloadServiceDbContext>(sp => sp.GetRequiredService<DownloadServiceDbContext>());

        builder.EnrichNpgsqlDbContext<DownloadServiceDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.DisableMetrics = false;
                settings.DisableTracing = false;
                settings.DisableHealthChecks = false;
            });
    }

    public void InstallServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IDownloadServiceRepository<>), typeof(DownloadServiceRepository<>));
        services.AddScoped<IDownloadServiceUow, DownloadServiceUow>();

        services.AddScoped<ISabNzbdServiceApi, SabNzbdServiceApi>();
        services.AddScoped<ISabNzbdClient, SabNzbdClient>();
        services.AddScoped<IDownloadServiceApi, DownloadServiceApi>();

        services.AddScoped<IDownloadClientRepository, DownloadClientRepository>();

        services.AddHttpClient<SabNzbdClient>();

        services.AddUserServiceApplication(typeof(IDownloadServiceApplication).Assembly);
    }
}
