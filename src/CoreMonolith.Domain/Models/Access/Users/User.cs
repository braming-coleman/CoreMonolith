using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.SharedKernel.Models;

namespace CoreMonolith.Domain.Models.Access.Users;

public class User : Entity
{
    public Guid Id { get; set; }
    public Guid? ExternalId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<UserPermissionGroup> UserPermissionGroups { get; set; } = [];
}
