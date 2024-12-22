using CoreMonolith.Infrastructure;
using CoreMonolith.Infrastructure.Clients.HttpClients.Access;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;

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
            .AddCookie()
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

                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var accessClient = context.HttpContext.RequestServices.GetRequiredService<AccessApiClient>();
                        var token = context.TokenEndpointResponse!.AccessToken;
                        var principle = context.Principal!;

                        var callbackResult = await accessClient.AuthCallbackAsync(
                            new AuthCallbackRequest(
                                new Guid(principle.FindFirstValue(CustomClaimNames.NameIdentifier)!),
                                principle.FindFirstValue(CustomClaimNames.PreferredUsername)!,
                                principle.FindFirstValue(CustomClaimNames.Givenname)!,
                                principle.FindFirstValue(CustomClaimNames.Surname)!,
                                //TODO: Just for testing
                                true),
                            token,
                            default)!;

                        if (callbackResult!.UserId != Guid.Empty)
                        {
                            var claimsIdentity = ((ClaimsIdentity)principle.Identity!);

                            claimsIdentity.AddClaim(new(CustomClaimNames.InternalUserId, callbackResult.UserId.ToString()));

                            foreach (var permission in callbackResult.Permissions)
                                claimsIdentity.AddClaim(new(CustomClaimNames.InternalPermission, permission));
                        }
                    }
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
