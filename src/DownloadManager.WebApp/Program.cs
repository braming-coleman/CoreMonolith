using CoreMonolith.Infrastructure;
using CoreMonolith.ServiceDefaults.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using DownloadManager.WebApp.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAndConfigureSerilog();

// Add services to the container.
builder.Services
    .AddOutputCache()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

//Shared Services
builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddHttpContextAccessor()
    .AddCustomHttpClients();

builder.AddRedisClient(connectionName: ConnectionNameConstants.RedisConnectionName);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace DownloadManager.WebApp
{
    public partial class Program;
}