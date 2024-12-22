using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class UserPermissionGroupRepository(ApplicationDbContext _dbContext)
    : Repository<UserPermissionGroup>(_dbContext), IUserPermissionGroupRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<HashSet<string>> GetPermissionsByExternalId(Guid externalId, CancellationToken cancellationToken = default)
    {
        var groupIds = await _dbContext
            .UserPermissionGroups.AsNoTracking()
            .Include(i => i.PermissionGroup).AsNoTracking()
            .Include(i => i.User).AsNoTracking()
            .Where(p => p.User!.ExternalId == externalId)
            .Select(s => s.PermissionGroupId)
            .ToListAsync(cancellationToken);

        var results = await _dbContext
            .PermissionGroupPermissions.AsNoTracking()
            .Include(i => i.Permission).AsNoTracking()
            .Where(p => groupIds.Contains(p.PermissionGroupId))
            .Select(s => s.Permission!.Key)
            .Distinct()
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (results is not null)
            permissions = results;

        return permissions;
    }

    public async Task<HashSet<string>> GetPermissionsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var groupIds = await _dbContext
            .UserPermissionGroups.AsNoTracking()
            .Include(i => i.PermissionGroup).AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(s => s.PermissionGroupId)
            .ToListAsync(cancellationToken);

        var results = await _dbContext
            .PermissionGroupPermissions.AsNoTracking()
            .Include(i => i.Permission).AsNoTracking()
            .Where(p => groupIds.Contains(p.PermissionGroupId))
            .Select(s => s.Permission!.Key)
            .Distinct()
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (results is not null)
            permissions = results;

        return permissions;
    }
}
