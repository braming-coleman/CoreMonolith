using CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;

namespace CoreMonolith.Api.Endpoints.V1.Access.Permissions;

internal sealed class Create : IEndpoint
{
    public sealed record PermissionCreateRequest(string Key, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/permission", Versions.V1)
            .MapPost("/create", async (PermissionCreateRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreatePermissionCommand(
                    request.Key,
                    request.Description);

                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.PermissionWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.Access);
    }
}
