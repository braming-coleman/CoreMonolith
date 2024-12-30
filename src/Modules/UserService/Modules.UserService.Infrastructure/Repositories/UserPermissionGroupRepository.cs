using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

public class UserPermissionGroupRepository(UserServiceDbContext _dbContext)
    : UserServiceRepository<UserPermissionGroup>(_dbContext), IUserPermissionGroupRepository
{
    private readonly UserServiceDbContext _dbContext = _dbContext;

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
