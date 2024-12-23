using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using DownloadManager.WebApp;
using DownloadManager.WebApp.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAndConfigureSerilog();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder
    .AddPresentation()
    .AddWebInfrastructure()
    .AddAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app
    .MapDefaultEndpoints()
    .MapLoginAndLogout();

app
    .UseRequestContextLogging()
    .UseSerilogRequestLogging()
    .UseExceptionHandler();

app
    .UseAuthentication()
    .UseAuthorization();

app.UseOutputCache();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace DownloadManager.WebApp
{
    public partial class Program;
}