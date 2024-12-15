using CoreMonolith.Domain.Access.Permissions;
using CoreMonolith.Domain.Access.UserPermissions;
using CoreMonolith.Domain.Access.Users;
using CoreMonolith.Domain.Todos;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<UserPermission> UserPermissions { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
