using CoreMonolith.Application.Abstractions.Services.Idempotency;
using CoreMonolith.Domain.Abstractions.Repositories;

namespace CoreMonolith.Infrastructure.Services.Idempotency;

public sealed class IdempotencyService(IUnitOfWork _unitOfWork)
    : IIdempotencyService
{
    public async Task CreateRequestAsync(Guid requestId, string name, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.IdempotencyRepository.CreateRequestAsync(requestId, name, cancellationToken);
    }

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.IdempotencyRepository.RequestExistsAsync(requestId, cancellationToken);
    }
}
