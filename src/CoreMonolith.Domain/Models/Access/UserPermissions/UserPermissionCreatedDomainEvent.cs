using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Models.Access.UserPermissions;

public sealed record UserPermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
