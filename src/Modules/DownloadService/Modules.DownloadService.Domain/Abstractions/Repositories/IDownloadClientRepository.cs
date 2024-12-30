using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Domain.Abstractions.Repositories;

public interface IDownloadClientRepository : IRepository<DownloadClient>
{
    Task<DownloadClient?> GetByTypeAsync(DownloadClientType type, CancellationToken cancellationToken = default);

    Task<bool> ExistsByType(DownloadClientType type, CancellationToken cancellationToken = default);
}
