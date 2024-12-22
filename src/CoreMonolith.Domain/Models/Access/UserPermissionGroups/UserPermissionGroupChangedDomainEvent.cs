using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Domain.Models.Access.UserPermissionGroups;

public sealed record UserPermissionGroupChangedDomainEvent(string Type) : IDomainEvent;
