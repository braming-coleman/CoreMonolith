﻿using CoreMonolith.Domain.Models.Access.Users;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}
