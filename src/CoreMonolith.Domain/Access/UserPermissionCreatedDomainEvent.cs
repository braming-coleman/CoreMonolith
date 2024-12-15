using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Access;

public sealed record UserPermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
