namespace Modules.UserService.Api.RequestModels;

public sealed record AuthenticationCallbackRequest(
    Guid ExternalId,
    string Email,
    string FirstName,
    string LastName,
    bool AdminUser = false);
