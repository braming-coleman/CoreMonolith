﻿using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class UserPermissionRepository(
    ApplicationDbContext _dbContext)
    : Repository<UserPermission>(_dbContext), IUserPermissionRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<bool> ExistsByUserAndPermissionIdAsync(Guid userId, Guid permissionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .UserPermissions.AsNoTracking()
            .AnyAsync(u => u.UserId == userId && u.PermissionId == permissionId,
            cancellationToken);
    }

    public async Task<HashSet<string>> GetPermissionsByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .UserPermissions.AsNoTracking()
            .Include(i => i.Permission).AsNoTracking()
            .Where(p => p.UserId == id)
            .Select(s => $"{s.Permission.Key}")
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (result is not null)
            permissions = result;

        return permissions;
    }
}
