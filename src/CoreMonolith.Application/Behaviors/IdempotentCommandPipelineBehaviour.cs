using CoreMonolith.Application.Abstractions.Idempotency;
using CoreMonolith.Application.Abstractions.Idempotency.Services;
using MediatR;

namespace CoreMonolith.Application.Behaviors;

internal sealed class IdempotentCommandPipelineBehaviour<TRequest, TResponse>(
    IIdempotencyService _service)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotentCommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (await _service.RequestExistsAsync(request.RequestId, cancellationToken))
            return default!;

        await _service.CreateRequestAsync(request.RequestId, typeof(TRequest).Name, cancellationToken);

        var response = await next();

        return response;
    }
}
