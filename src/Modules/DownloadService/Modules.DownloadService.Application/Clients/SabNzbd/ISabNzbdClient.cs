using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public interface ISabNzbdClient
{
    Task ConfigureAsync(SabNzbdClientSettings settings);

    Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request,
        CancellationToken cancellationToken = default);
}
