using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Application.Clients.SabNzbd;

namespace Modules.DownloadService.Infrastructure.Services;

internal sealed class SabNzbdServiceApi(ISabNzbdClient _client)
    : ISabNzbdServiceApi
{
    public Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
