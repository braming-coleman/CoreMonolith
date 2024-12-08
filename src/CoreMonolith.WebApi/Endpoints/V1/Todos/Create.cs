using CoreMonolith.Application.Todos.Create;
using CoreMonolith.Domain.Todos;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
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
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/todos")
            .HasApiVersion(Versions.V1)
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
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.Todos);
    }
}
