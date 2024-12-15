using CoreMonolith.Application.Access.Users;
using CoreMonolith.Application.Access.Users.GetById;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/user", Versions.V1)
            .MapGet("/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(userId);

                Result<UserResponse> result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.UserRead)
            .RequireAuthorization()
            .Produces<UserResponse>()
            .WithTags(Tags.Access)
            .CacheAuthedOutput(Tags.User);
    }
}
