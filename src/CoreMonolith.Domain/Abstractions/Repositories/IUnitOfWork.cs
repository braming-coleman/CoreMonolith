using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Abstractions.Repositories.Idempotency;

namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    IAccessContainer Access { get; }

    public IIdempotentRequestRepository IdempotencyRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
