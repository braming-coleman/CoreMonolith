using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class UserRepository(
    ApplicationDbContext _dbContext,
    IUnitOfWork _unitOfWork)
    : Repository<User>(_dbContext), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        return result;
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext
            .Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        return result;
    }

    public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
