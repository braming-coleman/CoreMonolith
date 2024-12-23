using CoreMonolith.ApiGateway;
using CoreMonolith.Application;
using CoreMonolith.Infrastructure;
using CoreMonolith.SharedKernel.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder
    .AddAndConfigureSerilog()
    .AddApiGatewayInfrastructure();

// Add services to the container
builder.Services.AddApplication();

// Add YARP configuration
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

builder
    .AddAuth()
    .AddPresentation();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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

app.Run();
