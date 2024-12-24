namespace CoreMonolith.Application.Abstractions.Idempotency.Services;

public interface IIdempotencyService
{
    Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default);

    Task CreateRequestAsync(Guid requestId, string name, CancellationToken cancellationToken = default);
}
