using CoreMonolith.Application.Abstractions.Idempotency;
using CoreMonolith.Application.Abstractions.Idempotency.Services;
using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;

namespace CoreMonolith.Application.Behaviors;

internal sealed class IdempotentCommandPipelineBehaviour<TRequest, TResponse>(
    IIdempotencyService _service)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).BaseType == typeof(IdempotentCommand<Guid>))
        {
            var internalRequest = (request as IdempotentCommand<Guid>)!;

            if (await _service.RequestExistsAsync(internalRequest.RequestId, cancellationToken))
                return (TResponse)(object)Result.Success(Guid.Empty);

            await _service.CreateRequestAsync(internalRequest.RequestId, typeof(TRequest).Name, cancellationToken);
        }

        var response = await next();

        return response;
    }
}
