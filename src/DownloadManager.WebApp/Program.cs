using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using DownloadManager.WebApp.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAndConfigureSerilog();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

//Shared Services
builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddCustomHttpClients();

// Add Keycloak authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "DownloadManager.WebApp.Auth";
})
.AddKeycloakOpenIdConnect(ConnectionNameConstants.KeycloakConnectionName, "core_monolith", options =>
{
    options.ClientId = "download-manager-web-app";
    options.ClientSecret = "OYSEhUd5Mg1tiLfTU96OiavuAVmmrhkM";
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.CallbackPath = "/authentication/login-callback";

    options.SaveTokens = true;
    //options.GetClaimsFromUserInfoEndpoint = true;
    options.DisableTelemetry = false;
    options.RequireHttpsMetadata = false;

    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("api-access");
    options.Scope.Add("web-app-access");
});

builder.AddRedisClients();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    IdentityModelEventSource.ShowPII = true;
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.MapEndpoints();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseOutputCache();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace DownloadManager.WebApp
{
    public partial class Program;
}