using CoreMonolith.Domain.Models.Access.PermissionGroupPermissions;
using CoreMonolith.SharedKernel.Models;

namespace CoreMonolith.Domain.Models.Access.Permissions;

public class Permission : Entity
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }

    public List<PermissionGroupPermission> PermissionGroupPermissions { get; set; } = [];
}
