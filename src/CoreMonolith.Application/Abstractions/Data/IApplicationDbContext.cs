using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.Domain.Models.Access.UserPermissions;
using CoreMonolith.Domain.Models.Access.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<UserPermission> UserPermissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
