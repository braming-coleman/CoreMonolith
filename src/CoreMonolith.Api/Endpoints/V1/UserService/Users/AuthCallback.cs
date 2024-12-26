using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Modules.UserService.Api;
using Modules.UserService.Api.RequestModels;
using Modules.UserService.Api.ResponseModels;

namespace CoreMonolith.Api.Endpoints.V1.UserService.Users;

internal sealed class AuthCallback : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("user-service", Versions.V1)
            .MapPost("/auth-callback", async (
                AuthenticationCallbackRequest request,
                IUserServiceApi userService,
                CancellationToken cancellationToken) =>
            {
                var result = await userService.AuthenticationCallbackAsync(request, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces<UserResponse>()
            .WithTags(Tags.UserService);
    }
}
