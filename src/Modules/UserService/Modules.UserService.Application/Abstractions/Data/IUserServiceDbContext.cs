using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Models.PermissionGroupPermissions;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.Permissions;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Application.Abstractions.Data;

public interface IUserServiceDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserPermissionGroup> UserPermissionGroups { get; }
    DbSet<PermissionGroup> PermissionGroups { get; }
    DbSet<PermissionGroupPermission> PermissionGroupPermissions { get; }
    DbSet<Permission> Permissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
