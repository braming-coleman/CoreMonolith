using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Abstractions.Idempotency;

public abstract record IdempotentCommand<TResponse>(Guid RequestId) : ICommand<TResponse>;
