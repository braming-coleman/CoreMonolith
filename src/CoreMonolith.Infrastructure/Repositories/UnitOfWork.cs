using CoreMonolith.Domain.Abstractions.Repositories;

namespace CoreMonolith.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
