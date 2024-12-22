using CoreMonolith.Domain.Models.Access.UserPermissionGroups;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserPermissionGroupRepository : IRepository<UserPermissionGroup>
{
    Task<HashSet<string>> GetPermissionsByUserId(Guid userId, CancellationToken cancellationToken = default);
}
