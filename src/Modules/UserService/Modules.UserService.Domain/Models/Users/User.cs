using CoreMonolith.Domain.Models;
using Modules.UserService.Domain.Models.UserPermissionGroups;

namespace Modules.UserService.Domain.Models.Users;

public class User : Entity
{
    public Guid Id { get; set; }
    public Guid? ExternalId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<UserPermissionGroup> UserPermissionGroups { get; set; } = [];
}
