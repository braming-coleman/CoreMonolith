using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Modules.UserService.Api;

namespace CoreMonolith.Api.Endpoints.V1.UserService.UserPermissionGroups;

internal sealed class GetPermissionsByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("user-service/user", Versions.V1)
            .MapGet("/{userId}/permissions", async (Guid userId, IUserServiceApi userService, CancellationToken cancellationToken) =>
            {
                var result = await userService.PermissionsGetByUserIdAsync(userId, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionRead)
            .RequireAuthorization()
            .Produces<HashSet<string>>()
            .WithTags(Tags.UserService)
            .CacheAuthedOutput(Tags.UserPermission);
    }
}
