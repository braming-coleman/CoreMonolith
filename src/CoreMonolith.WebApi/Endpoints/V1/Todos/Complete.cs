using CoreMonolith.Application.Todos.Complete;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("todos", Versions.V1)
            .MapPut("/{id:guid}/complete", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CompleteTodoCommand(id);

                Result result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Todos);
    }
}
