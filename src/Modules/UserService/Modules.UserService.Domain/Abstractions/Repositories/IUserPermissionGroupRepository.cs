using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.UserPermissionGroups;

namespace Modules.UserService.Domain.Abstractions.Repositories;

public interface IUserPermissionGroupRepository : IRepository<UserPermissionGroup>
{
    Task<HashSet<string>> GetPermissionsByUserId(Guid userId, CancellationToken cancellationToken = default);

    Task<HashSet<string>> GetPermissionsByExternalId(Guid externalId, CancellationToken cancellationToken = default);
}
