using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Application.Abstractions.Idempotency.Services;
using CoreMonolith.Application.Abstractions.Modules;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Infrastructure.Authentication;
using CoreMonolith.Infrastructure.Authorization;
using CoreMonolith.Infrastructure.Clients.HttpClients;
using CoreMonolith.Infrastructure.Clients.HttpClients.UserService;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.Infrastructure.Repositories;
using CoreMonolith.Infrastructure.Services.Idempotency;
using CoreMonolith.Infrastructure.Time;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.OutputCaching;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

namespace CoreMonolith.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }

    public static WebApplicationBuilder AddApiInfrastructure(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies)
    {
        builder.Services.AddApiServices();

        builder.AddDatabase();

        builder.InstallModuleInfrastructure(assemblies);

        builder.AddHealthChecks();

        builder.AddRabbitMqClient();

        builder.AddRedisClients();

        return builder;
    }

    public static WebApplicationBuilder AddWebInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddRedisClients();

        builder.Services.AddCustomHttpClients();

        return builder;
    }

    public static WebApplicationBuilder InstallModuleInfrastructure(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies)
    {
        var installers = GetInstallers(assemblies);

        foreach (var installer in installers)
        {
            installer.InstallServices(builder.Services, builder.Configuration);

            installer.InstallDatabase(builder);
        }

        return builder;
    }

    public static void ApplyMigrations(
        this IApplicationBuilder app,
        params Assembly[] assemblies)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using CoreMonolithDbContext dbContext = scope.ServiceProvider.GetRequiredService<CoreMonolithDbContext>();

        dbContext.Database.Migrate();

        var installers = GetInstallers(assemblies);

        foreach (var installer in installers)
            installer.ApplyMigrations(app);
    }

    public static WebApplicationBuilder AddAndConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostContext, config) =>
        {
            config.WriteTo.Console();
            config.WriteTo.OpenTelemetry();
        });

        return builder;
    }

    private static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration
            .GetConnectionString(ConnectionNameConstants.CoreMonolithDbName)!;

        builder.Services.AddDbContext<CoreMonolithDbContext>(
            options => options
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default);
                    })
                .UseSnakeCaseNamingConvention());

        builder.Services.AddScoped<ICoreMonolithDbContext>(sp => sp.GetRequiredService<CoreMonolithDbContext>());

        builder.EnrichNpgsqlDbContext<CoreMonolithDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.DisableMetrics = false;
                settings.DisableTracing = false;
                settings.DisableHealthChecks = false;
            });

        return builder;
    }

    private static IEnumerable<IModuleServicesInstaller> GetInstallers(params Assembly[] assemblies)
    {
        var results = assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(IsAssignableToType<IModuleServicesInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IModuleServicesInstaller>();

        return results;

        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsInterface &&
            !typeInfo.IsAbstract;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddMapster();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IIdempotentRequestRepository, IdempotentRequestRepository>();
        services.AddScoped<IIdempotencyService, IdempotencyService>();

        return services;
    }

    private static WebApplicationBuilder AddRabbitMqClient(this WebApplicationBuilder builder)
    {
        builder.AddRabbitMQClient(connectionName: ConnectionNameConstants.RabbitMqConnectionName, options =>
        {
            options.DisableTracing = false;
            options.DisableHealthChecks = false;
        });

        return builder;
    }

    private static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddNpgSql();

        return builder;
    }

    private static WebApplicationBuilder AddRedisClients(this WebApplicationBuilder builder)
    {
        builder.AddRedisClient(connectionName: ConnectionNameConstants.RedisConnectionName, options =>
        {
            options.DisableTracing = false;
            options.DisableHealthChecks = false;
        });

        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(b => b.AddPolicy<CustomPolicy>().SetCacheKeyPrefix("custom-"), true);
        });

        builder.Services.AddOutputCache();

        builder.AddRedisOutputCache(connectionName: ConnectionNameConstants.RedisConnectionName, options =>
        {
            options.DisableTracing = false;
            options.DisableHealthChecks = false;
        });

        return builder;
    }

    private static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
    {
        services.AddTransient<KeycloakTokenHandler>();

        var apiGatewayUri = new Uri($"https+http://{ConnectionNameConstants.ApiGatewayConnectionName}");

        services.AddHttpClient<WeatherApiClient>(c => { c.BaseAddress = apiGatewayUri; }).AddHttpMessageHandler<KeycloakTokenHandler>();
        services.AddHttpClient<UserServiceApiClient>(c => { c.BaseAddress = apiGatewayUri; }).AddHttpMessageHandler<KeycloakTokenHandler>();

        return services;
    }
}
