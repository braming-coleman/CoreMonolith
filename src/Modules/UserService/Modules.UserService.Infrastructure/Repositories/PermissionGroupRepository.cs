using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;
public class PermissionGroupRepository(UserServiceDbContext _dbContext)
    : Repository<PermissionGroup>(_dbContext), IPermissionGroupRepository
{
    private readonly UserServiceDbContext _dbContext = _dbContext;

    public async Task<PermissionGroup?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .PermissionGroups.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }
}
