using CoreMonolith.Domain.Models.Access.PermissionGroups;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IPermissionGroupRepository : IRepository<PermissionGroup>
{
    Task<PermissionGroup?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
}
