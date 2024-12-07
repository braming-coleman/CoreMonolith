using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemCompletedDomainEvent(Guid TodoItemId) : IDomainEvent;
