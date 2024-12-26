using CoreMonolith.SharedKernel.Errors;

namespace Modules.UserService.Domain.Models.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "User.NotFound",
        $"The user with the Id = '{userId}' was not found.");

    public static Error NotFoundByEmail(string email) => Error.NotFound(
        "User.NotFoundByEmail",
        $"The user with the specified email '{email}' was not found.");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "User.EmailNotUnique",
        "The provided email is not unique.");

    public static readonly Error CreationFailed = Error.Conflict(
        "User.CreationFailed",
        "Unable to create user.");
}
