using CoreMonolith.SharedKernel.ValueObjects;

namespace Modules.DownloadService.Api;

public interface IDownloadServiceApi
{
    Task<Result<Guid>> CreateDownloadClient();
}
