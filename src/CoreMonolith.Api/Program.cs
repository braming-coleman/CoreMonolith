using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.WebApi;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAndConfigureSerilog();

builder.AddApiInfrastructure();

builder.Services
    .AddApplication()
    .AddEndpoints(Assembly.GetExecutingAssembly());

builder.AddAuth();

builder.AddPresentation();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();

    app.UseDefaultOpenApi();
}

app
    .UseRequestContextLogging()
    .UseSerilogRequestLogging()
    .UseExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseOutputCache();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace CoreMonolith.WebApi
{
    public partial class Program;
}
