﻿using CoreMonolith.Domain.Models.Access.Permissions;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByKeyAsync(string key, CancellationToken cancellationToken = default);
}
