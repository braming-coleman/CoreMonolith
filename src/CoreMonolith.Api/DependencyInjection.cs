using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;

namespace CoreMonolith.WebApi;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.AddDefaultOpenApi();

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
}
