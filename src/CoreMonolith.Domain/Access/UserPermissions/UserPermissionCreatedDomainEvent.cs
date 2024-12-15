using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Access.UserPermissions;

public sealed record UserPermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
