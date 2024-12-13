using CoreMonolith.Domain.Abstractions.Repositories.Access;

namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    IAccessContainer Access { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
