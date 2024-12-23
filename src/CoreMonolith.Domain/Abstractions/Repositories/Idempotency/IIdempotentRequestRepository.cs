﻿using CoreMonolith.Domain.Models.Idempotency;

namespace CoreMonolith.Domain.Abstractions.Repositories.Idempotency;

public interface IIdempotentRequestRepository : IRepository<IdempotentRequest>
{
    Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default);
}
