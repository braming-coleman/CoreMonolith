using CoreMonolith.Domain.Access;

namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}
