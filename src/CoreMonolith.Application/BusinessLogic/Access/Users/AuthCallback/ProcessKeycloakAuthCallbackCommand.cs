using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

public sealed record ProcessKeycloakAuthCallbackCommand(
    Guid ExternalId,
    string Email,
    string FirstName,
    string LastName,
    bool AdminUser = false)
    : ICommand<ProcessKeycloakAuthCallbackResult>;