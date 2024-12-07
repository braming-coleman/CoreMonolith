using CoreMonolith.Domain.Todos;
using CoreMonolith.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
