using CoreMonolith.ApiGateway;
using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
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
    .AddGatewayInfrastructure(assemblies);

// Add services to the container
builder.Services.AddApplication();

// Add YARP configuration
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

builder
    .AddPresentation()
    .AddAuth();

var app = builder.Build();

app
    .MapDefaultEndpoints()
    .MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations(assemblies);

    app.UseSwaggerDocs();
}

app
    .UseRequestContextLogging()
    .UseSerilogRequestLogging()
    .UseExceptionHandler()
    .UseAuthentication()
    .UseAuthorization();

// Enable YARP
app.MapReverseProxy();

await app.RunAsync();
