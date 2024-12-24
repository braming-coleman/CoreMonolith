using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Abstractions.Repositories.Idempotency;
using CoreMonolith.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace CoreMonolith.Infrastructure.Repositories;

public class UnitOfWork(
    ApplicationDbContext _dbContext,
    IServiceProvider _serviceProvider)
    : IUnitOfWork, IDisposable
{
    private bool _disposed = false;

    public IAccessContainer Access => _serviceProvider.GetRequiredService<IAccessContainer>();
    public IIdempotentRequestRepository IdempotencyRepository => _serviceProvider.GetRequiredService<IIdempotentRequestRepository>();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _dbContext.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

