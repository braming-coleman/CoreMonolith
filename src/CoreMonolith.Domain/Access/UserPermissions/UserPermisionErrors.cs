using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Access.UserPermissions;

public static class UserPermisionErrors
{
    public static Error NotFound(Guid Id) => Error.NotFound(
        "UserPermision.NotFound",
        $"The user-permision with the Id = '{Id}' was not found.");

    public static readonly Error ExistsByUserAndPermissionId = Error.NotFound(
        "UserPermision.ExistsByUserAndPermissionId",
        "This UserId and PermissionId combination already exists.");
}
