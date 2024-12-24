using CoreMonolith.ApiGateway;
using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder
    .AddAndConfigureSerilog()
    .AddApiInfrastructure();

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
    app.ApplyMigrations();

    app.UseSwaggerDocs(builder.Configuration);
}

app
    .UseRequestContextLogging()
    .UseSerilogRequestLogging()
    .UseExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseOutputCache();

// Enable YARP
app.MapReverseProxy();

await app.RunAsync();
