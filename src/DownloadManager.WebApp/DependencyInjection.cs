using CoreMonolith.Infrastructure;
using CoreMonolith.Infrastructure.Clients.HttpClients.Access;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.JsonWebTokens;
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
        var oidcScheme = OpenIdConnectDefaults.AuthenticationScheme;

        builder.Services
            .AddAuthenticationContext()
            .AddAuthentication(oidcScheme)
            .AddKeycloakOpenIdConnect(
                serviceName: ConnectionNameConstants.KeycloakConnectionName,
                realm: builder.Configuration["OpenIdConnect:Realm"]!,
                authenticationScheme: oidcScheme,
                configureOptions: options =>
            {
                options.ClientId = builder.Configuration["OpenIdConnect:ClientId"];
                options.ClientSecret = builder.Configuration[ConfigKeyConstants.WebAppClientSecret];
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.DisableTelemetry = false;

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("core-api-access");
                options.Scope.Add("core-api-gateway-access");
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

                        if (callbackResult is not null
                        && callbackResult!.UserId != Guid.Empty)
                        {
                            var claimsIdentity = ((ClaimsIdentity)principle.Identity!);

                            claimsIdentity.AddClaim(new(CustomClaimNames.InternalUserId, callbackResult.UserId.ToString()));

                            foreach (var permission in callbackResult.Permissions)
                                claimsIdentity.AddClaim(new(CustomClaimNames.InternalPermission, permission));
                        }
                    }
                };

            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        builder.Services.AddCascadingAuthenticationState();

        return builder;
    }

    internal static IEndpointConventionBuilder MapLoginAndLogout(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("authentication");

        group.MapGet(pattern: "/login", OnLogin).AllowAnonymous();
        group.MapPost(pattern: "/logout", OnLogout);

        return group;
    }

    static ChallengeHttpResult OnLogin() =>
        TypedResults.Challenge(properties: new AuthenticationProperties
        {
            RedirectUri = "/"
        });

    static SignOutHttpResult OnLogout() =>
        TypedResults.SignOut(properties: new AuthenticationProperties
        {
            RedirectUri = "/"
        },
        [
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        ]);
}
