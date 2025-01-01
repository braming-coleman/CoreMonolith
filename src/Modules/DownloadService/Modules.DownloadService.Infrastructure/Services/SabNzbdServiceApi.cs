using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetConfig;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetFullStatus;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetQueue;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetVersion;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.UploadNewNzb;

namespace Modules.DownloadService.Infrastructure.Services;

internal sealed class SabNzbdServiceApi(ISender _sender)
    : ISabNzbdServiceApi
{
    public async Task<IResult> HandGetRequestAsync(GetRequest request, CancellationToken cancellationToken = default)
    {
        //version
        if (request.Mode == SabNzbdCommands.Version)
        {
            var query = new GetApiVersionQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return CustomResults.Problem(Result.Failure<VersionResponse>(result.Error));

            return Results.Ok(result.Value.Response);
        }
        //get_config
        else if (request.Mode == SabNzbdCommands.GetConfig)
        {
            var query = new GetApiConfigQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return CustomResults.Problem(Result.Failure<ConfigResponse>(result.Error));

            return Results.Ok(result.Value.Response);
        }
        //fullstatus
        else if (request.Mode == SabNzbdCommands.FullStatus)
        {
            var query = new GetApiFullStatusQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return CustomResults.Problem(Result.Failure<FullStatusResponse>(result.Error));

            return Results.Ok(result.Value.Response);
        }
        //queue
        else if (request.Mode == SabNzbdCommands.Queue)
        {
            var query = new GetApiQueueQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return CustomResults.Problem(Result.Failure<QueueResponse>(result.Error));

            return Results.Ok(result.Value.Response);
        }

        return CustomResults.Problem(Result.Failure<object>(SabNzbdClientErrors.ModeUnsupported(request.Mode)));
    }

    public async Task<IResult> UploadNzbAsync(PostRequest request, CancellationToken cancellationToken = default)
    {
        var fileName = string.IsNullOrEmpty(request.NzbName) ? request.Name.FileName : request.NzbName;
        var priority = string.IsNullOrEmpty(request.Priority) ? SabNzdbDefaults.Priority : Enum.Parse<Priority>(request.Priority);
        var pp = string.IsNullOrEmpty(request.Pp) ? SabNzdbDefaults.PostProcessingOptions : Enum.Parse<PostProcessingOptions>(request.Pp);
        var cat = string.IsNullOrEmpty(request.Cat) ? SabNzdbDefaults.Category : request.Cat;

        using MemoryStream stream = new();
        await request.Name.CopyToAsync(stream, cancellationToken);

        var file = stream.ToArray();

        var uploadRequest = new NzbUploadRequest(
            request.ApiKey,
            file,
            fileName, cat, priority, pp);

        var command = new UploadNewNzbCommand(uploadRequest);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return CustomResults.Problem(Result.Failure<NzbUploadResponse>(result.Error));

        return Results.Ok(result.Value.UploadResult);
    }
}
