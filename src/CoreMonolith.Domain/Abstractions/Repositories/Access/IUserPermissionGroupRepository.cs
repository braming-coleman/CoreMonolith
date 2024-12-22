using CoreMonolith.Domain.Models.Access.UserPermissionGroups;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserPermissionGroupRepository : IRepository<UserPermissionGroup>
{
    Task<HashSet<string>> GetPermissionsByUserId(Guid userId, CancellationToken cancellationToken = default);

    Task<HashSet<string>> GetPermissionsByExternalId(Guid externalId, CancellationToken cancellationToken = default);
}
