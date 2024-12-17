using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Models.Access.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
