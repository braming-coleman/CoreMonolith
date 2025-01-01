using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public interface ISabNzbdClient
{
    Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request,
        SabNzbdClientSettings settings,
        CancellationToken cancellationToken = default);

    Task<Result<T>> GetAsync<T>(
        GetRequest request,
        SabNzbdClientSettings settings,
        Dictionary<string, string?>? additionalParams = default,
        CancellationToken cancellationToken = default)
        where T : class;
}
