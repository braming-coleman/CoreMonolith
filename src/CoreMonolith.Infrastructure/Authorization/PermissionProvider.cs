using CoreMonolith.Domain.Abstractions.Repositories;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionProvider(IUnitOfWork _unitOfWork)
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        return await _unitOfWork
            .Access
            .UserPermissionRepository
            .GetPermissionsForUserIdAsync(userId);
    }
}
