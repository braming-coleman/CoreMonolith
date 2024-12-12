using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
