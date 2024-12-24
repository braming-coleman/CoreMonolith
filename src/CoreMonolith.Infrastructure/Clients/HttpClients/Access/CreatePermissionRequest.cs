namespace CoreMonolith.Infrastructure.Clients.HttpClients.Access;

public sealed record CreatePermissionRequest(
    string Key,
    string Description);
