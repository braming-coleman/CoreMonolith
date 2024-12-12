using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;
