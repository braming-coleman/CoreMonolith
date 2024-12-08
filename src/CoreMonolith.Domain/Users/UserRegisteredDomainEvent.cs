using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
