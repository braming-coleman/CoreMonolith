using CoreMonolith.Domain.Models;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.Permissions;

namespace Modules.UserService.Domain.Models.PermissionGroupPermissions;

public class PermissionGroupPermission : Entity
{
    public Guid Id { get; set; }
    public Guid PermissionGroupId { get; set; }
    public Guid PermissionId { get; set; }

    public PermissionGroup? PermissionGroup { get; set; }
    public Permission? Permission { get; set; }
}
