using CoreMonolith.Domain.Access;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserPermissionRepository : IRepository<UserPermission>
{
    Task<HashSet<string>> GetPermissionsByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserAndPermissionIdAsync(Guid userId, Guid permissionId, CancellationToken cancellationToken = default);
}
