﻿using CoreMonolith.Application.Users.Register;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Users;

internal sealed class Register : IEndpoint
{
    public sealed record UserRegisterRequest(string Email, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/users")
            .HasApiVersion(Versions.V1)
            .MapPost("/register", async (UserRegisterRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new RegisterUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Password);

                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .Produces<Guid>()
            .WithTags(Tags.Users);
    }
}
