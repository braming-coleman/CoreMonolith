using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Models;

namespace CoreMonolith.Domain.Models.Access.UserPermissionGroups;

public class UserPermissionGroup : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionGroupId { get; set; }

    public User? User { get; set; }
    public PermissionGroup? PermissionGroup { get; set; }
}
