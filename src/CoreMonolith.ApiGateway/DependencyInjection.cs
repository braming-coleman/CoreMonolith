using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Infrastructure;

namespace CoreMonolith.ApiGateway;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }

    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication()
            .AddKeycloakJwtBearer(
                ConnectionNameConstants.KeycloakConnectionName,
                realm: builder.Configuration["OpenIdConnect:Realm"]!,
                options =>
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
