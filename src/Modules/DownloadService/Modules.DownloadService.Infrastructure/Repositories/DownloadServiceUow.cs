using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Infrastructure.Database;

namespace Modules.DownloadService.Infrastructure.Repositories;

internal sealed class DownloadServiceUow(DownloadServiceDbContext _dbContext)
    : IDownloadServiceUow
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

