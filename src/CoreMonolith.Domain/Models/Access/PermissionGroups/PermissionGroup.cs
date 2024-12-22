using CoreMonolith.Domain.Models.Access.PermissionGroupPermissions;
using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.SharedKernel.Models;

namespace CoreMonolith.Domain.Models.Access.PermissionGroups;

public class PermissionGroup : Entity
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public List<PermissionGroupPermission> PermissionGroupPermissions { get; set; } = [];
    public List<UserPermissionGroup> UserPermissionGroups { get; set; } = [];
}
