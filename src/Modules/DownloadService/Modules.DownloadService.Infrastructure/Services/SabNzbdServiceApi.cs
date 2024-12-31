using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetVersion;
using Modules.DownloadService.Application.BusinessLogic.SabNzbd.UploadNewNzb;

namespace Modules.DownloadService.Infrastructure.Services;

internal sealed class SabNzbdServiceApi(ISender _sender)
    : ISabNzbdServiceApi
{
    public async Task<Result<VersionResponse>> HandGetRequestAsync(GetRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Mode == SabNzbdCommands.Version)
        {
            var query = new GetApiVersionQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.Value.Response;
        }

        return Result.Failure<VersionResponse>(SabNzbdClientErrors.ModeUnsupported(request.Mode));
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
