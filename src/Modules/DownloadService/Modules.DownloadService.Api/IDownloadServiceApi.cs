using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Models;

namespace Modules.DownloadService.Api;

public interface IDownloadServiceApi
{
    Task<Result<Guid>> CreateDownloadClientAsync(Guid requestId, DownloadClientCreateRequest request, CancellationToken cancellationToken = default);
}
