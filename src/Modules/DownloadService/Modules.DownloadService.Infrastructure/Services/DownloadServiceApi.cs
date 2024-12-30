using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;
using Modules.DownloadService.Api;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Application.BusinessLogic.DownloadClients.Create;

namespace Modules.DownloadService.Infrastructure.Services;

internal sealed class DownloadServiceApi(
    ISender _sender) : IDownloadServiceApi
{
    public Task<Result<Guid>> CreateDownloadClientAsync(
        Guid requestId,
        DownloadClientCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateDownloadClientCommand(
            requestId,
            request.ClientType,
            request.Name,
            request.Enabled,
            request.Config);

        var result = _sender.Send(command, cancellationToken);

        return result;
    }
}
