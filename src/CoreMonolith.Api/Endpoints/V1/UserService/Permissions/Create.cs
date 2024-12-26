using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Modules.UserService.Api;
using Modules.UserService.Api.RequestModels;

namespace CoreMonolith.Api.Endpoints.V1.UserService.Permissions;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("user-service/permission", Versions.V1)
            .MapPost("/create", async (
                [FromHeader(Name = EndpointConstants.IdempotencyHeaderKeyName)] string requestId,
                PermissionRequest request,
                IUserServiceApi userService,
                CancellationToken cancellationToken) =>
            {
                if (!Guid.TryParse(requestId, out var parsedRequestId))
                    return Results.BadRequest();

                var result = await userService.PermissionCreateAsync(parsedRequestId, request, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.UserService);
    }
}
