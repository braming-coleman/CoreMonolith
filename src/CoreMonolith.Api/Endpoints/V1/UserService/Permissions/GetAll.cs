using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Modules.UserService.Api;
using Modules.UserService.Api.ResponseModels;

namespace CoreMonolith.Api.Endpoints.V1.UserService.Permissions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("user-service/permission", Versions.V1)
            .MapGet("/all", async (IUserServiceApi userService, CancellationToken cancellationToken) =>
            {
                var result = await userService.PermissionsGetAll(cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionRead)
            .RequireAuthorization()
            .Produces<List<PermissionResponse>>()
            .WithTags(Tags.UserService)
            .CacheAuthedOutput(Tags.Permission);
    }
}
