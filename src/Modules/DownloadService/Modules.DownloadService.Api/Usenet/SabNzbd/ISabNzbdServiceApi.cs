using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public interface ISabNzbdServiceApi
{
    Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default);

    Task<Result<VersionResponse>> HandGetRequestAsync(GetRequest request, CancellationToken cancellationToken = default);
}
