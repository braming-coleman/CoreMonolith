using CoreMonolith.Domain.Access;

namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IAccessRepository : IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}
