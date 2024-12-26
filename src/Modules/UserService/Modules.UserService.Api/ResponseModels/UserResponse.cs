namespace Modules.UserService.Api.ResponseModels;

public sealed record UserResponse(
    Guid UserId,
    Guid? ExternalId,
    string Email,
    HashSet<string> Permissions);
