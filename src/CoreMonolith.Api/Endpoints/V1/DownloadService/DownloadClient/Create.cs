using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Modules.DownloadService.Api;
using Modules.DownloadService.Api.Models;

namespace CoreMonolith.Api.Endpoints.V1.DownloadService.DownloadClient;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("download-service/download-client", Versions.V1)
            .MapPost("/create", async (
                [FromHeader(Name = EndpointConstants.IdempotencyHeaderKeyName)] string requestId,
                DownloadClientCreateRequest request,
                IDownloadServiceApi downloadService,
                CancellationToken cancellationToken) =>
            {
                if (!Guid.TryParse(requestId, out var parsedRequestId))
                    return Results.BadRequest();

                var result = await downloadService.CreateDownloadClientAsync(parsedRequestId, request, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.DownloadClientWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.DownloadService);
    }
}
