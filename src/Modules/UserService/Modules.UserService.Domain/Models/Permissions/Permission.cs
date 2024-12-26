using CoreMonolith.Domain.Models;
using Modules.UserService.Domain.Models.PermissionGroupPermissions;

namespace Modules.UserService.Domain.Models.Permissions;

public class Permission : Entity
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }

    public List<PermissionGroupPermission> PermissionGroupPermissions { get; set; } = [];
}
