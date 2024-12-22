using CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.PermissionGroups;

internal sealed class AuthCallback : IEndpoint
{
    public sealed record AuthCallbackRequest(
        Guid ExternalId,
        string Email,
        string FirstName,
        string LastName,
        bool IsAdmin);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/permission-group", Versions.V1)
            .MapPost("/auth-callback", async (AuthCallbackRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new ProcessKeycloakAuthCallbackCommand(
                    request.ExternalId,
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.IsAdmin);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces<ProcessKeycloakAuthCallbackResult>()
            .WithTags(Tags.Access);
    }
}
