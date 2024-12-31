using Microsoft.EntityFrameworkCore;
using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Infrastructure.Database;

namespace Modules.DownloadService.Infrastructure.Repositories;

public class DownloadServiceRepository<T>(DownloadServiceDbContext _dbContext)
    : IDownloadServiceRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = _dbContext.Set<T>();

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<T?> FindByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([Id], cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var result = await FindByIdAsync(Id, cancellationToken);

        return result is not null;
    }
}
