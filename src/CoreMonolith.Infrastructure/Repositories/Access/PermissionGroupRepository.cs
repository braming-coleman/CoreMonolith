using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;
public class PermissionGroupRepository(ApplicationDbContext _dbContext)
    : Repository<PermissionGroup>(_dbContext), IPermissionGroupRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<PermissionGroup?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .PermissionGroups.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }
}
