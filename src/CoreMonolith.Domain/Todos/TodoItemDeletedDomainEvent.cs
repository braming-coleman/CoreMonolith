using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemDeletedDomainEvent(Guid TodoItemId) : IDomainEvent;
