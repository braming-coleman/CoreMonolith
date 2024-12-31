using CoreMonolith.SharedKernel.Infrastructure;
using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetConfig;
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
            var query = new GetApiGetConfigQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return CustomResults.Problem(Result.Failure<ConfigResponse>(result.Error));

            return Results.Ok(result.Value.Response);
        }

        return CustomResults.Problem(Result.Failure<VersionResponse>(SabNzbdClientErrors.ModeUnsupported(request.Mode)));
    }

    public async Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default)
    {
        var command = new UploadNewNzbCommand(request);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<NzbUploadResponse>(result.Error);

        return result.Value.UploadResult;
    }
}
