using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Infrastructure.Authentication;
using CoreMonolith.Infrastructure.Authorization;
using CoreMonolith.Infrastructure.Clients.HttpClients;
using CoreMonolith.Infrastructure.Clients.HttpClients.Access;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.Infrastructure.Repositories;
using CoreMonolith.Infrastructure.Repositories.Access;
using CoreMonolith.Infrastructure.Time;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.OutputCaching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

    public static WebApplicationBuilder AddApiInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiServices();

        builder.AddDatabase();

        builder.AddRabbitMqClient();

        builder.AddRedisClients();

        builder.Services.AddHealthChecks(builder.Configuration);

        return builder;
    }

    public static WebApplicationBuilder AddWebInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddRedisClients();

        builder.Services.AddCustomHttpClients();

        return builder;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccessContainer, AccessContainer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IUserPermissionGroupRepository, UserPermissionGroupRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration
            .GetConnectionString(ConnectionNameConstants.DbConnStringName);

        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        builder.EnrichNpgsqlDbContext<ApplicationDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.DisableMetrics = false;
                settings.DisableTracing = false;
                settings.DisableHealthChecks = false;
            });

        return builder;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString(ConnectionNameConstants.DbConnStringName)!);

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

        services.AddHttpClient<WeatherApiClient>(client =>
            {
                client.BaseAddress = new($"https+http://{ConnectionNameConstants.ApiConnectionName}");
            })
            .AddHttpMessageHandler<KeycloakTokenHandler>();

        services.AddHttpClient<AccessApiClient>(client =>
            {
                client.BaseAddress = new($"https+http://{ConnectionNameConstants.ApiConnectionName}");
            })
            .AddHttpMessageHandler<KeycloakTokenHandler>();

        return services;
    }
}
