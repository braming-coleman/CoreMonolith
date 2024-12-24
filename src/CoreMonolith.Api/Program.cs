using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.WebApi;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddServiceDefaults()
    .AddAndConfigureSerilog()
    .AddApiInfrastructure();

builder.Services
    .AddApplication()
    .AddEndpoints(Assembly.GetExecutingAssembly());

builder
    .AddAuth()
    .AddPresentation();

var app = builder.Build();

app
    .MapDefaultEndpoints()
    .MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs(builder.Configuration);
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
