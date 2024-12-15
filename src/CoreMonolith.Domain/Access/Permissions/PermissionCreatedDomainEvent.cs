using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Access.Permissions;

public sealed record class PermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
