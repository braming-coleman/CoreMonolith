using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public interface ISabNzbdClient
{
    Task<Result<UploadReponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default);
}
