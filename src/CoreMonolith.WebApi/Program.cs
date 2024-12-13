using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.WebApi;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAndConfigureSerilog();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddEndpoints(Assembly.GetExecutingAssembly());

builder
    .EnrichDbContext()
    .AddInfrastructureClients()
    .AddDefaultOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();

    app.UseDefaultOpenApi();
}

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace CoreMonolith.WebApi
{
    public partial class Program;
}
