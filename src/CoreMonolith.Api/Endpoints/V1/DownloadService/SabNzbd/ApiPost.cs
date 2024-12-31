using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace CoreMonolith.Api.Endpoints.V1.DownloadService.SabNzbd;

public class PostRequest
{
    [FromForm(Name = "output")]
    public string Output { get; set; }

    [FromForm(Name = "apikey")]
    public string ApiKey { get; set; }

    [FromForm(Name = "mode")]
    public string Mode { get; set; }

    [FromForm(Name = "name")]
    public IFormFile Name { get; set; }

    [FromForm(Name = "nzbname")]
    public string NzbName { get; set; }

    [FromForm(Name = "pp")]
    public string Pp { get; set; }

    [FromForm(Name = "priority")]
    public string Priority { get; set; }

    [FromForm(Name = "cat")]
    public string Cat { get; set; }
}

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
                if (string.IsNullOrEmpty(formBody.ApiKey))
                    return Results.BadRequest("'apikey' not found.");

                if (string.IsNullOrEmpty(formBody.Mode))
                    return Results.BadRequest("'mode' not found.");

                if (formBody.Name is null)
                    return Results.BadRequest("'name' not found.");

                return await HandleMode(formBody, _serviceApi, cancellationToken);
            })
            .AllowAnonymous()
            .DisableAntiforgery()
            .Produces<NzbUploadResponse>()
            .WithTags(Tags.DownloadService);
    }

    private static async Task<IResult> HandleMode(
        PostRequest request,
        ISabNzbdServiceApi _serviceApi,
        CancellationToken cancellationToken)
    {
        if (request.Mode == SabNzbdCommands.AddFile)
        {
            var result = await UploadNzb(request, _serviceApi, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }

        return CustomResults.Problem(
            Result.Failure<Result<object>>(SabNzbdClientErrors.ModeUnsupported(request.Mode)));
    }

    private static async Task<Result<NzbUploadResponse>> UploadNzb(
        PostRequest request,
        ISabNzbdServiceApi _serviceApi,
        CancellationToken cancellationToken)
    {

        using MemoryStream stream = new();
        await request.Name.CopyToAsync(stream, cancellationToken);

        var file = stream.ToArray();

        var fileName = string.IsNullOrEmpty(request.NzbName) ?
            request.Name.FileName :
            request.NzbName;

        var priority = string.IsNullOrEmpty(request.Priority) ?
            SabNzdbDefaults.Priority :
            Enum.Parse<Priority>(request.Priority);

        var pp = string.IsNullOrEmpty(request.Pp) ?
            SabNzdbDefaults.PostProcessingOptions :
            Enum.Parse<PostProcessingOptions>(request.Pp);

        var cat = string.IsNullOrEmpty(request.Cat) ?
            SabNzdbDefaults.Category :
            request.Cat;

        var uploadRequest = new NzbUploadRequest(
            request.ApiKey,
            file,
            fileName, cat, priority, pp);

        var result = await _serviceApi.UploadNzbAsync(uploadRequest, cancellationToken);

        return result.Match(
            () => result,
            (e) => Result.Failure<NzbUploadResponse>(result.Error));
    }
}
