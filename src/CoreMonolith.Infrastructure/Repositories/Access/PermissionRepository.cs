using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access.Permissions;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class PermissionRepository(ApplicationDbContext _dbContext)
    : Repository<Permission>(_dbContext), IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<bool> ExistsByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Permissions.AsNoTracking()
            .AnyAsync(p => p.Key == key, cancellationToken);
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext
            .Permissions.AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
