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

    public async Task<bool> ExistsByType(DownloadClientType type, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .DownloadClients.AsNoTracking()
            .AnyAsync(p => p.Type == type, cancellationToken);
    }

    public async Task<DownloadClient?> GetByTypeAsync(DownloadClientType type, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .DownloadClients.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Type == type, cancellationToken);
    }
}
