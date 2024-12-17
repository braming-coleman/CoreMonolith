using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Models.Access.Permissions;

public static class PermissionErrors
{
    public static Error NotFound(Guid Id) => Error.NotFound(
        "Permission.NotFound",
        $"The permission with the Id = '{Id}' was not found.");

    public static readonly Error ExistsByKey = Error.Conflict(
        "Permission.ExistsByKey",
        "Duplicate permission key.");
}
