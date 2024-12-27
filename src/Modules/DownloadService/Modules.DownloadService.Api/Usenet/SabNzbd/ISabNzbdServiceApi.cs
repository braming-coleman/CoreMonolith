using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public interface ISabNzbdServiceApi
{
    Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default);
}
