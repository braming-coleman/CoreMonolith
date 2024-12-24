using CoreMonolith.Domain.Abstractions.Repositories.Idempotency;
using CoreMonolith.Domain.Models.Idempotency;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Idempotency;

public class IdempotentRequestRepository(ApplicationDbContext _dbContext)
    : Repository<IdempotentRequest>(_dbContext), IIdempotentRequestRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.IdempotentRequests.AnyAsync(p => p.Id == requestId, cancellationToken);
    }
}
