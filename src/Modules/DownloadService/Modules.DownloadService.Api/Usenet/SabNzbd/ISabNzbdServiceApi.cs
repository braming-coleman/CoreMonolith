using CoreMonolith.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Http;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public interface ISabNzbdServiceApi
{
    Task<Result<NzbUploadResponse>> UploadNzbAsync(NzbUploadRequest request, CancellationToken cancellationToken = default);

    Task<IResult> HandGetRequestAsync(GetRequest request, CancellationToken cancellationToken = default);
}
