using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public interface ISabNzbdClient
{
    Task<Result<UploadReponse>> UploadNzbAsync(
        Guid downloadClientId,
        NzbUploadRequest request,
        CancellationToken cancellationToken = default);
}
