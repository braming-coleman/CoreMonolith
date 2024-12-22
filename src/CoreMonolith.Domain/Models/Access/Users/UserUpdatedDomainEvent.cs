using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Models.Access.Users;

public sealed record UserUpdatedDomainEvent(Guid UserId) : IDomainEvent;
