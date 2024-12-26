using CoreMonolith.Domain.Abstractions.Messaging;

namespace Modules.UserService.Domain.Models.Users;

public sealed record UserUpdatedDomainEvent(Guid UserId) : IDomainEvent;
