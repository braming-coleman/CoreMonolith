using CoreMonolith.Domain.Models;
using Modules.UserService.Domain.Models.PermissionGroupPermissions;
using Modules.UserService.Domain.Models.UserPermissionGroups;

namespace Modules.UserService.Domain.Models.PermissionGroups;

public class PermissionGroup : Entity
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public List<PermissionGroupPermission> PermissionGroupPermissions { get; set; } = [];
    public List<UserPermissionGroup> UserPermissionGroups { get; set; } = [];
}
