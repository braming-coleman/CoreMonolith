using CoreMonolith.SharedKernel.Errors;

namespace Modules.UserService.Domain.Models.PermissionGroups;

public static class PermissionGroupErrors
{
    public static Error NotFound(string name) => Error.NotFound(
        "PermissionGroup.NotFound",
        $"PermissionGroup '{name}' not found.");
}
