using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

internal sealed class UnitOfWork(UserServiceDbContext _dbContext)
    : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

