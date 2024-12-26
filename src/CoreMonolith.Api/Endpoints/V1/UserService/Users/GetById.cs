using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Modules.UserService.Api;
using Modules.UserService.Api.ResponseModels;

namespace CoreMonolith.Api.Endpoints.V1.UserService.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("user-service/user", Versions.V1)
            .MapGet("/{userId}", async (Guid userId, IUserServiceApi userService, CancellationToken cancellationToken) =>
            {
                var result = await userService.UserGetByIdAsync(userId, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.UserRead)
            .RequireAuthorization()
            .Produces<UserResponse?>()
            .WithTags(Tags.UserService)
            .CacheAuthedOutput(Tags.User);
    }
}
