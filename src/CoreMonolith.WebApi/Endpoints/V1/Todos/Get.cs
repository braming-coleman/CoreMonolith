using CoreMonolith.Application.Todos.Get;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/todos")
            .HasApiVersion(Versions.V1)
            .MapGet("/", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GetTodosQuery(userId);

                Result<List<TodoResponse>> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces<List<TodoResponse>>()
            .WithTags(Tags.Todos);
    }
}
