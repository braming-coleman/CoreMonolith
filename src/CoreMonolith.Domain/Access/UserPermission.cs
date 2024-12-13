namespace CoreMonolith.Domain.Access;

public class UserPermission
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }

    public User? User { get; set; }
    public Permission? Permission { get; set; }
}
