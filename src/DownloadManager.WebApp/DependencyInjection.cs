using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace DownloadManager.WebApp;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        return builder;
    }

    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthenticationContext()
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = builder.Configuration["OpenIdConnect:CookieName"];
            })
            .AddKeycloakOpenIdConnect(
                ConnectionNameConstants.KeycloakConnectionName,
                builder.Configuration["OpenIdConnect:Realm"]!,
                options =>
            {
                options.ClientId = builder.Configuration["OpenIdConnect:ClientId"];
                options.ClientSecret = builder.Configuration[ConfigKeyConstants.WebAppClientSecret];
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.CallbackPath = builder.Configuration["OpenIdConnect:CallbackPath"];

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.DisableTelemetry = false;
                options.RequireHttpsMetadata = false;

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("core-api-access");
                options.Scope.Add("download-web-access");
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
