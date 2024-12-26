using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.WebApi;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assemblies = Directory
    .GetFiles(AppContext.BaseDirectory, "*.dll")
    .Select(Assembly.LoadFrom)
    .Where(assembly => assembly.FullName!.StartsWith("Modules."))
    .ToArray();

builder
    .AddServiceDefaults()
    .AddAndConfigureSerilog()
    .AddApiInfrastructure(assemblies);

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
    app.UseSwaggerDocs();
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
namespace CoreMonolith.Api
{
    public partial class Program;
}
