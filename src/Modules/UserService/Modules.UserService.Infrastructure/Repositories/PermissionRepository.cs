using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Permissions;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

public class PermissionRepository(UserServiceDbContext _dbContext)
    : Repository<Permission>(_dbContext), IPermissionRepository
{
    private readonly UserServiceDbContext _dbContext = _dbContext;

    public async Task<bool> ExistsByKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Permissions.AsNoTracking()
            .AnyAsync(p => p.Key == key, cancellationToken);
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Permissions.AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
