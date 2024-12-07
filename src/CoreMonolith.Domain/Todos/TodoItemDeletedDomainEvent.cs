using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;
