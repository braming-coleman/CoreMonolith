using CoreMonolith.Domain.Abstractions.Messaging;

namespace Modules.UserService.Domain.Models.UserPermissionGroups;

public sealed record UserPermissionGroupChangedDomainEvent(string Type) : IDomainEvent;
