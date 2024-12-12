using CoreMonolith.Application.Todos.GetById;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("todos", Versions.V1)
            .MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GetTodoByIdQuery(id);

                Result<TodoResponse> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .Produces<TodoResponse>()
            .WithTags(Tags.Todos);
    }
}
