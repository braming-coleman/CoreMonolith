using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Idempotency;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories;

public class IdempotentRequestRepository(CoreMonolithDbContext _dbContext)
    : Repository<IdempotentRequest>(_dbContext), IIdempotentRequestRepository
{
    private readonly CoreMonolithDbContext _dbContext = _dbContext;

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.IdempotentRequests.AnyAsync(p => p.Id == requestId, cancellationToken);
    }
}
