using CoreMonolith.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Permissions;

namespace Modules.UserService.Domain.Abstractions.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByKeyAsync(string key, CancellationToken cancellationToken = default);
}
