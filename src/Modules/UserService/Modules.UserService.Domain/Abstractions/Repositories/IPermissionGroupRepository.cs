using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.PermissionGroups;

namespace Modules.UserService.Domain.Abstractions.Repositories;

public interface IPermissionGroupRepository : IRepository<PermissionGroup>
{
    Task<PermissionGroup?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
}
