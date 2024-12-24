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
            .AddProblemDetails()
            .AddSwaggerGen();

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
                    options.TokenValidationParameters.ValidateIssuer = false;
                });

        builder.Services
            .AddAuthenticationContext()
            .AddApiAuthorization();

        return builder;
    }

    public static WebApplication UseSwaggerDocs(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/core-api/swagger/v1/swagger.json", "V1 Documentation");
            c.SwaggerEndpoint($"/core-api/swagger/v2/swagger.json", "V2 Documentation");
        });

        return app;
    }
}
