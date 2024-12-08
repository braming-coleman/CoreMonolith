using CoreMonolith.Application.Users.Login;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Users;

internal sealed class Login : IEndpoint
{
    public sealed record UserLoginRequest(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/users")
            .HasApiVersion(Versions.V1)
            .MapPost("/login", async (UserLoginRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new LoginUserCommand(request.Email, request.Password);

                Result<string> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<string>()
            .WithTags(Tags.Users);
    }
}
