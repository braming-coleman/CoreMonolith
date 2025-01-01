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
    public class FormPart
    {
        [FromForm(Name = "name")]
        public IFormFile Name { get; set; }

        [FromForm(Name = "nzbname")]
        public string NzbName { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("download-service", Versions.V1)
            .MapPost("/intercept/sabnzbd/api", async (
                [FromQuery(Name = "output")] string output,
                [FromQuery(Name = "apikey")] string apiKey,
                [FromQuery(Name = "mode")] string mode,
                [FromForm] FormPart formBody,
                [FromQuery(Name = "pp")] string? pp,
                [FromQuery(Name = "priority")] string? priority,
                [FromQuery(Name = "cat")] string? cat,
                ISabNzbdServiceApi _serviceApi,
                CancellationToken cancellationToken) =>
            {
                if (mode != SabNzbdCommands.AddFile)
                    return CustomResults.Problem(Result.Failure<Result<object>>(SabNzbdClientErrors.ModeUnsupported(mode)));

                var request = new PostRequest
                {
                    Mode = mode,
                    ApiKey = apiKey,
                    Name = formBody.Name,
                    Cat = cat,
                    NzbName = formBody.NzbName,
                    Output = output,
                    Pp = pp,
                    Priority = priority
                };

                return await _serviceApi.UploadNzbAsync(request, cancellationToken);
            })
            .AllowAnonymous()
            .DisableAntiforgery()
            .Produces<NzbUploadResponse>()
            .WithTags(Tags.DownloadService);
    }
}