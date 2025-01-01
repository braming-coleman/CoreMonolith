using CoreMonolith.Api.Swagger.Examples.SabNzbd;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Modules.DownloadService.Api;
using Modules.DownloadService.Api.Models;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace CoreMonolith.Api.Endpoints.V1.DownloadService.DownloadClient;

public class CreateRequest
{
    [Required]
    public DownloadClientType ClientType { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public bool Enabled { get; set; }

    [Required]
    public object Config { get; set; }
}

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("download-service/download-client", Versions.V1)
            .MapPost("/create",
                [SwaggerRequestExample(typeof(CreateRequest), typeof(CreateDownloadClientResponseExamples))] async (
                [FromHeader(Name = EndpointConstants.IdempotencyHeaderKeyName)] string requestId,
                CreateRequest request,
                IDownloadServiceApi downloadService,
                CancellationToken cancellationToken) =>
            {
                if (!Guid.TryParse(requestId, out var parsedRequestId))
                    return Results.BadRequest();

                var command = new DownloadClientCreateRequest(
                    request.ClientType,
                    request.Name,
                    request.Enabled,
                    request.Config);

                var result = await downloadService.CreateDownloadClientAsync(parsedRequestId, command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(ApiPermissions.DownloadClientWrite)
            .RequireAuthorization()
            .Produces<Guid>()
            .WithTags(Tags.DownloadService);
    }
}
