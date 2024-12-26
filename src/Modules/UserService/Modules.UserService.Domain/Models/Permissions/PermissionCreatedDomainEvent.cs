using CoreMonolith.Domain.Abstractions.Messaging;

namespace Modules.UserService.Domain.Models.Permissions;

public sealed record class PermissionCreatedDomainEvent(Guid Id) : IDomainEvent;
