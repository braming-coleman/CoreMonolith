using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories;

public class AccessRepository(
    IApplicationDbContext _dbContext,
    IUnitOfWork _unitOfWork)
    : IAccessRepository
{
    public void Add<T>(T entity)
    {
        throw new NotImplementedException();
    }

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

    public void Remove<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
