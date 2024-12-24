using CoreMonolith.Domain.Models.Access.PermissionGroupPermissions;
using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserPermissionGroup> UserPermissionGroups { get; }
    DbSet<PermissionGroup> PermissionGroups { get; }
    DbSet<PermissionGroupPermission> PermissionGroupPermissions { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<IdempotentRequest> IdempotentRequests { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
