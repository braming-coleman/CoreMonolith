using CoreMonolith.Domain.Access;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserPermissionRepository : IRepository<UserPermission>
{
    Task<HashSet<string>> GetPermissionsForUserIdAsync(Guid id, CancellationToken cancellationToken = default);
}
