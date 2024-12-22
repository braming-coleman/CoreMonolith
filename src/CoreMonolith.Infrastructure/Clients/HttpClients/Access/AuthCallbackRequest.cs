namespace CoreMonolith.Infrastructure.Clients.HttpClients.Access;

public sealed record AuthCallbackRequest(
    Guid ExternalId,
    string Email,
    string FirstName,
    string LastName,
    bool IsAdmin);
