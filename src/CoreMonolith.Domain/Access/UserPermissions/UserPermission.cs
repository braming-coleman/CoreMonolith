using CoreMonolith.Domain.Access.Permissions;
using CoreMonolith.Domain.Access.Users;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Access.UserPermissions;

public class UserPermission : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }

    public User? User { get; set; }
    public Permission? Permission { get; set; }
}
