using CoreMonolith.Domain.Models;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Domain.Models.UserPermissionGroups;

public class UserPermissionGroup : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionGroupId { get; set; }

    public User? User { get; set; }
    public PermissionGroup? PermissionGroup { get; set; }
}
