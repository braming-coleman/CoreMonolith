namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

public sealed record ProcessKeycloakAuthCallbackResult(
    Guid UserId,
    HashSet<string> Permissions);
