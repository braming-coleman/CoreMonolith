using CoreMonolith.Application.BusinessLogic.Access.Permissions;
using CoreMonolith.Application.BusinessLogic.Access.Permissions.GetAll;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.Api.Endpoints.V1.Access.Permissions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/permission", Versions.V1)
            .MapGet("/get-all", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetAllPermissionsQuery();

                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionRead)
            .RequireAuthorization()
            .Produces<List<PermissionReposnse>>()
            .WithTags(Tags.Access)
            .CacheAuthedOutput(Tags.Permission);
    }
}
