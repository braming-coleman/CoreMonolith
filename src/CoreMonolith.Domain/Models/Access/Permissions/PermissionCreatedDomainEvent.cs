using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Models.Access.Permissions;

public sealed record class PermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
