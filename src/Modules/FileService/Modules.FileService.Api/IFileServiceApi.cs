using CoreMonolith.SharedKernel.ValueObjects;

namespace Modules.FileService.Api;

public interface IFileServiceApi
{
    Task<Result<Guid>> TrackFileAsync(string DownloadClientId, CancellationToken cancellationToken);
}
