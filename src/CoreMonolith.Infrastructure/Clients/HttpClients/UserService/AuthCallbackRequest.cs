namespace CoreMonolith.Infrastructure.Clients.HttpClients.UserService;

public sealed record AuthCallbackRequest(
    Guid ExternalId,
    string Email,
    string FirstName,
    string LastName,
    bool AdminUser);
