using CoreMonolith.Domain.Access.UserPermissions;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Access.Users;

public class User : Entity
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PasswordHash { get; set; }

    public List<UserPermission> UserPermissions { get; set; } = [];
}
