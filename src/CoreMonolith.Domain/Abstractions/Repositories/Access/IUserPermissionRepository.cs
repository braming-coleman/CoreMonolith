using CoreMonolith.Domain.Models.Access.UserPermissions;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserPermissionRepository : IRepository<UserPermission>
{
    Task<HashSet<string>> GetPermissionsByUserIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<HashSet<string>> GetPermissionsByExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default);

    Task<bool> ExistsByUserAndPermissionIdAsync(Guid userId, Guid permissionId, CancellationToken cancellationToken = default);
}
