using CoreMonolith.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.UserPermissionGroups;

internal sealed class GetPermissionsByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/permission-group", Versions.V1)
            .MapGet("/permissions/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetPermissionsByUserIdQuery(userId);

                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionRead)
            .RequireAuthorization()
            .Produces<HashSet<string>>()
            .WithTags(Tags.Access)
            .CacheAuthedOutput(Tags.UserPermission);
    }
}
