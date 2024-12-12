using CoreMonolith.Application.Todos.Get;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("todo", Versions.V1)
            .MapGet("/", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GetTodosQuery(userId);

                Result<List<TodoResponse>> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.TodoRead)
            .RequireAuthorization()
            .Produces<List<TodoResponse>>()
            .WithTags(Tags.Todo);
    }
}
