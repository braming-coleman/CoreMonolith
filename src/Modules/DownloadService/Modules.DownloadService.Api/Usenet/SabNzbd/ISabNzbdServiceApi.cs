using Microsoft.AspNetCore.Http;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public interface ISabNzbdServiceApi
{
    Task<IResult> UploadNzbAsync(PostRequest request, CancellationToken cancellationToken = default);

    Task<IResult> HandGetRequestAsync(GetRequest request, CancellationToken cancellationToken = default);
}
