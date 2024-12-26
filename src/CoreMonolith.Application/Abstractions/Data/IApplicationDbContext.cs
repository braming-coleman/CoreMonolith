using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<IdempotentRequest> IdempotentRequests { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
