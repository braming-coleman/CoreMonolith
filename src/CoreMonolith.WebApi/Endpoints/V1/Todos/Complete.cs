﻿using CoreMonolith.Application.Todos.Complete;
using CoreMonolith.SharedKernel;
using CoreMonolith.WebApi.Extensions;
using CoreMonolith.WebApi.Infrastructure;
using MediatR;

namespace CoreMonolith.WebApi.Endpoints.V1.Todos;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/todos")
            .HasApiVersion(Versions.V1)
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
