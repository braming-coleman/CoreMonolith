using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Infrastructure.Database;

namespace CoreMonolith.Infrastructure.Repositories;

internal sealed class UnitOfWork(CoreMonolithDbContext _dbContext)
    : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

