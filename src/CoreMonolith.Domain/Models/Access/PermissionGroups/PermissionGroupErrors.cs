using CoreMonolith.SharedKernel.Errors;

namespace CoreMonolith.Domain.Models.Access.PermissionGroups;
public static class PermissionGroupErrors
{
    public static Error NotFound(string name) => Error.NotFound(
        "PermissionGroup.NotFound",
        $"PermissionGroup '{name}' not found.");
}
