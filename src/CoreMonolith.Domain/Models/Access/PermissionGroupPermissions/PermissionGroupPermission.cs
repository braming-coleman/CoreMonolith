using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Domain.Models.Access.Permissions;

namespace CoreMonolith.Domain.Models.Access.PermissionGroupPermissions;

public class PermissionGroupPermission
{
    public Guid Id { get; set; }
    public Guid PermissionGroupId { get; set; }
    public Guid PermissionId { get; set; }

    public PermissionGroup? PermissionGroup { get; set; }
    public Permission? Permission { get; set; }
}
