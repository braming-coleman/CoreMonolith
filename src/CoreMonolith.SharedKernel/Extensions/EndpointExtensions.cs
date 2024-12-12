using CoreMonolith.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace CoreMonolith.SharedKernel.Extensions;

public static class EndpointExtensions
{
    public static RouteGroupBuilder MapApiVersion(this IEndpointRouteBuilder builder, string resource, int version)
    {
        return builder
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/" + resource)
            .HasApiVersion(version);
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
            endpoint.MapEndpoint(builder);

        return app;
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder builder, string permission)
    {
        return builder.RequireAuthorization(permission);
    }
}
