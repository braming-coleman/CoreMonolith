namespace CoreMonolith.Infrastructure.Clients.HttpClients.UserService;

public sealed record CreatePermissionRequest(
    string Key,
    string Description);
