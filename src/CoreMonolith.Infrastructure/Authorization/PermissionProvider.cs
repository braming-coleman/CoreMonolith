using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionProvider(ApplicationDbContext _dbContext)
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        return await _dbContext
            .UserPermissions
            .Include(i => i.Permission)
            .Where(p => p.UserId == userId)
            .Select(s => s.Permission.Key)
            .ToHashSetAsync();
    }
}
