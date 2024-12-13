using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Access;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
