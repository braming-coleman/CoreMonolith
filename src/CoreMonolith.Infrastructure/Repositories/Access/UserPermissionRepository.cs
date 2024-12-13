using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class UserPermissionRepository(
    ApplicationDbContext _dbContext,
    IUnitOfWork _unitOfWork)
    : Repository<UserPermission>(_dbContext), IUserPermissionRepository
{
    public async Task<HashSet<string>> GetPermissionsByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .UserPermissions
            .Include(i => i.Permission)
            .Where(p => p.UserId == id)
            .Select(s => $"{s.Permission.Key}")
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (result is not null)
            permissions = result;

        return permissions;
    }
}
