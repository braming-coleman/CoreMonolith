using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class PermissionRepository(ApplicationDbContext _dbContext)
    : Repository<Permission>(_dbContext), IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext
            .Permissions.AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
