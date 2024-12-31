using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public interface ISabNzbdClient
{
    Task ConfigureAsync(SabNzbdClientSettings settings);

    Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request, CancellationToken cancellationToken = default);

    Task<Result<VersionResponse>> GetVerionAsync(
        GetRequest request, CancellationToken cancellationToken = default);
}
