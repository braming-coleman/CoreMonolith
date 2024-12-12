namespace CoreMonolith.Domain.Access;

public sealed class UserPermission
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
}
