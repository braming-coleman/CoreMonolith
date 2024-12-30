using Microsoft.EntityFrameworkCore;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Domain.Models.DownloadClients;
using Modules.DownloadService.Infrastructure.Database;

namespace Modules.DownloadService.Infrastructure.Repositories;

public class DownloadClientRepository(DownloadServiceDbContext _dbContext) :
    DownloadServiceRepository<DownloadClient>(_dbContext), IDownloadClientRepository
{
    private readonly DownloadServiceDbContext _dbContext = _dbContext;

    public Task<DownloadClient?> SabNzbdGetClientDetailsAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext
            .DownloadClients.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Type == DownloadClientType.SabNzbd, cancellationToken);
    }
}
