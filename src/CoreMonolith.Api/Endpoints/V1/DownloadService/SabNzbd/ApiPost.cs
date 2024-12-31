using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

namespace CoreMonolith.Api.Endpoints.V1.DownloadService.SabNzbd;

public class ApiPost : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("download-service", Versions.V1)
            .MapPost("/intercept/sabnzbd/api", async (
                [FromForm] PostRequest formBody,
                ISabNzbdServiceApi _serviceApi,
                CancellationToken cancellationToken) =>
            {
                if (formBody.Mode != SabNzbdCommands.AddFile)
                    return CustomResults.Problem(Result.Failure<Result<object>>(SabNzbdClientErrors.ModeUnsupported(formBody.Mode)));

                return await _serviceApi.UploadNzbAsync(formBody, cancellationToken);
            })
            .AllowAnonymous()
            .DisableAntiforgery()
            .Produces<NzbUploadResponse>()
            .WithTags(Tags.DownloadService);
    }
}
