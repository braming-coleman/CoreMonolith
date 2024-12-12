using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemCompletedDomainEvent(Guid TodoItemId) : IDomainEvent;
