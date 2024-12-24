using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Infrastructure;

namespace CoreMonolith.ApiGateway;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication()
            .AddKeycloakJwtBearer(
                serviceName: ConnectionNameConstants.KeycloakConnectionName,
                realm: builder.Configuration["OpenIdConnect:Realm"]!,
                configureOptions: options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = builder.Configuration["OpenIdConnect:Audience"];
                });

        builder.Services
            .AddAuthenticationContext()
            .AddApiAuthorization();

        return builder;
    }

    public static WebApplication UseSwaggerDocs(this WebApplication app, IConfiguration config)
    {
        var baseUrl = config[$"{ConnectionNameConstants.ApiConnectionName}-01:https:0"];

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"{baseUrl}/core-api/swagger/v1/swagger.json", "V1 Documentation");
            c.SwaggerEndpoint($"{baseUrl}/core-api/swagger/v2/swagger.json", "V2 Documentation");
        });

        return app;
    }
}
