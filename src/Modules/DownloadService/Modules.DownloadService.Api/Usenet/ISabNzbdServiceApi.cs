using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.Models;

namespace Modules.DownloadService.Api.Usenet;

public interface ISabNzbdServiceApi
{
    Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default);
}
