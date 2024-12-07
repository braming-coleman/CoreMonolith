using CoreMonolith.Application.Todos.Get;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.Todos;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("todos", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetTodosQuery(userId);

            Result<List<TodoResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization();
    }
}
