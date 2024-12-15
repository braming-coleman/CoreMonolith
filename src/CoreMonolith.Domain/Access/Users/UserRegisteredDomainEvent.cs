using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Access.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
