using CoreMonolith.Application.Access.UserPermissions.GetPermissionsByUserId;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.UserPermissions;

internal sealed class GetByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/user-permission", Versions.V1)
            .MapGet("/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetPermissionsByUserIdQuery(userId);

                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.UserPermissionRead)
            .RequireAuthorization()
            .Produces<HashSet<string>>()
            .WithTags(Tags.Access)
            .CacheAuthedOutput(Tags.UserPermission);
    }
}
