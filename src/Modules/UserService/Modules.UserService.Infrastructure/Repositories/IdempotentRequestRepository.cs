using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

public class IdempotentRequestRepository(UserServiceDbContext _dbContext)
    : Repository<IdempotentRequest>(_dbContext), IIdempotentRequestRepository
{
    private readonly UserServiceDbContext _dbContext = _dbContext;

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.IdempotentRequests.AnyAsync(p => p.Id == requestId, cancellationToken);
    }
}
