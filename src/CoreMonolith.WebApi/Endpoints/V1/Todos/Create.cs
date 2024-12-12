using CoreMonolith.Application.Todos.Create;
using CoreMonolith.Domain.Todos;
using CoreMonolith.SharedKernel;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class Create : IEndpoint
{
    public sealed class TodoCreateRequest
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public List<string> Labels { get; set; } = [];
        public int Priority { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("todo", Versions.V1)
            .MapPost("/", async (TodoCreateRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateTodoCommand
                {
                    UserId = request.UserId,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    Labels = request.Labels,
                    Priority = (Priority)request.Priority
                };

                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.TodoWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.Todo);
    }
}
