using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Infrastructure.Authentication;
using CoreMonolith.Infrastructure.Authorization;
using CoreMonolith.Infrastructure.Clients.HttpClients;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.Infrastructure.Repositories;
using CoreMonolith.Infrastructure.Repositories.Access;
using CoreMonolith.Infrastructure.Time;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System.Text;

namespace CoreMonolith.Infrastructure;

public static class DependencyInjection
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

    public static WebApplicationBuilder AddInfrastructureClients(this WebApplicationBuilder builder)
    {
        builder.AddRedisClient(connectionName: ConnectionNameConstants.RedisConnectionName);

        builder.AddRabbitMQClient(connectionName: ConnectionNameConstants.RabbitMqConnectionName);

        return builder;
    }

    public static WebApplicationBuilder EnrichDbContext(this WebApplicationBuilder builder)
    {
        builder.EnrichNpgsqlDbContext<ApplicationDbContext>(
            configureSettings: settings =>
            {
                settings.DisableRetry = false;
                settings.DisableMetrics = false;
                settings.DisableTracing = false;
                settings.DisableHealthChecks = false;
                settings.CommandTimeout = 30;
            });

        return builder;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddCustomAuthentication(configuration)
            .AddAuthorizationInternal();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccessContainer, AccessContainer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();

        return services;
    }

    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<WeatherApiClient>(client =>
             {
                 client.BaseAddress = new($"https+http://{ConnectionNameConstants.WebApiConnectionName}");
             })
            .AddResilienceHandler("custom", pipeline =>
            {
                pipeline.AddTimeout(TimeSpan.FromSeconds(5));

                pipeline.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    Delay = TimeSpan.FromMilliseconds(500)
                });

                pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
                {
                    SamplingDuration = TimeSpan.FromSeconds(10),
                    FailureRatio = 0.9,
                    MinimumThroughput = 5,
                    BreakDuration = TimeSpan.FromSeconds(5)
                });

                pipeline.AddTimeout(TimeSpan.FromSeconds(1));
            });

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(ConnectionNameConstants.DbConnStringName);

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString(ConnectionNameConstants.DbConnStringName)!);

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigKeyConstants.JwtSecretKeyName]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}
