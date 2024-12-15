using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Access;

public class Permission : Entity
{
    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string? Description { get; set; }

    public List<UserPermission> UserPermissions { get; set; } = [];
}
