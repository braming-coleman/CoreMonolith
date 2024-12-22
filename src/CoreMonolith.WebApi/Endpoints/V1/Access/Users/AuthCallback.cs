using CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;
using CoreMonolith.Infrastructure.Clients.HttpClients.Access;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.Users;

internal sealed class AuthCallback : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/user", Versions.V1)
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
