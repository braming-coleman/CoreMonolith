using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class UserRepository(ApplicationDbContext _dbContext)
    : Repository<User>(_dbContext), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        return result;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        return result;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Users.AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.ExternalId == externalId, cancellationToken);
    }
}
