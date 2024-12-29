using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Domain.Abstractions.Repositories;

public interface IDownloadClientRepository : IRepository<DownloadClient>
{
    //SabNzbd
    Task<DownloadClient?> SabNzbdGetClientDetailsAsync(CancellationToken cancellationToken = default);
}
