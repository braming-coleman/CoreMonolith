using CoreMonolith.Application.BusinessLogic.Access.UserPermissions.Create;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.UserPermissions;

internal sealed class Create : IEndpoint
{
    public sealed record UserPermissionCreateRequest(Guid UserId, Guid PermissionId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/user-permission", Versions.V1)
            .MapPost("/create", async (UserPermissionCreateRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateUserPermissionCommand(
                    request.UserId,
                    request.PermissionId);

                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.UserPermissionWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.Access);
    }
}
