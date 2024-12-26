using CoreMonolith.Domain.Abstractions.Messaging;

namespace Modules.UserService.Domain.Models.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
