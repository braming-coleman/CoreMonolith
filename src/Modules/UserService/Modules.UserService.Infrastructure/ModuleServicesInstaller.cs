﻿using CoreMonolith.Application.Abstractions.Idempotency.Services;
using CoreMonolith.Application.Abstractions.Modules;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.ServiceDefaults.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.UserService.Api;
using Modules.UserService.Application;
using Modules.UserService.Application.Abstractions.Data;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Infrastructure.Database;
using Modules.UserService.Infrastructure.Repositories;
using Modules.UserService.Infrastructure.Services;
using Modules.UserService.Infrastructure.Services.Idempotency;

namespace Modules.UserService.Infrastructure;

public class ModuleServicesInstaller : IModuleServicesInstaller
{
    public void ApplyMigrations(IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using UserServiceDbContext dbContext = scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();

        dbContext.Database.Migrate();
    }

    public void InstallDatabase(IHostApplicationBuilder builder)
    {
        string connectionString = builder.Configuration
            .GetConnectionString(ConnectionNameConstants.UserServiceDbName)!;

        builder.Services.AddDbContext<UserServiceDbContext>(
            options => options
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default);
                    })
                .UseSnakeCaseNamingConvention());

        builder.Services.AddScoped<IUserServiceDbContext>(sp => sp.GetRequiredService<UserServiceDbContext>());

        builder.EnrichNpgsqlDbContext<UserServiceDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.DisableMetrics = false;
                settings.DisableTracing = false;
                settings.DisableHealthChecks = false;
            });

        builder.Services
            .AddHealthChecks()
            .AddNpgSql(connectionString);
    }

    public void InstallServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IUserPermissionGroupRepository, UserPermissionGroupRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IIdempotentRequestRepository, IdempotentRequestRepository>();

        services.AddScoped<IIdempotencyService, IdempotencyService>();

        services.AddScoped<IUserServiceApi, UserServiceApi>();

        services.AddUserServiceApplication();
    }
}
