using CoreMonolith.Application.BusinessLogic.Access.Users.Login;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Access.Users;

internal sealed class Login : IEndpoint
{
    public sealed record UserLoginRequest(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("access/user", Versions.V1)
            .MapPost("/login", async (UserLoginRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new LoginUserCommand(request.Email, request.Password);

                Result<string> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<string>()
            .WithTags(Tags.Access);
    }
}
