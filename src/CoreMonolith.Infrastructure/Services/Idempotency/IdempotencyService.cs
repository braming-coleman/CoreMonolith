﻿using CoreMonolith.Application.Abstractions.Idempotency.Services;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Idempotency;

namespace CoreMonolith.Infrastructure.Services.Idempotency;

public sealed class IdempotencyService(IUnitOfWork _unitOfWork)
    : IIdempotencyService
{
    public async Task CreateRequestAsync(Guid requestId, string name, CancellationToken cancellationToken = default)
    {
        var request = new IdempotentRequest
        {
            Id = requestId,
            Name = name,
            Created = DateTimeOffset.UtcNow,
        };

        await _unitOfWork.IdempotencyRepository.AddAsync(request, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> RequestExistsAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.IdempotencyRepository.RequestExistsAsync(requestId, cancellationToken);
    }
}
