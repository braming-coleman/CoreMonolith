namespace Modules.UserService.Api.ResponseModels;

public sealed record PermissionResponse(
    Guid PermissionId,
    string Key,
    string Description);
