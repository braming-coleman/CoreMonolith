using Asp.Versioning;
using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.ServiceDefaults;
using CoreMonolith.WebApi;
using CoreMonolith.WebApi.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.EnrichNpgsqlDbContext<ApplicationDbContext>(
    configureSettings: settings =>
    {
        settings.DisableRetry = false;
        settings.DisableMetrics = false;
        settings.DisableTracing = false;
        settings.DisableHealthChecks = false;
        settings.CommandTimeout = 30;
    });

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var withApiVersioning = builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

// REMARK: If you want to use Controllers, you'll need this.
app.MapControllers();

app.UseDefaultOpenApi();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace CoreMonolith.WebApi
{
    public partial class Program;
}
