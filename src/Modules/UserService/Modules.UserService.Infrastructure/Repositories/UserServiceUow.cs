using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

internal sealed class UserServiceUow(UserServiceDbContext _dbContext)
    : IUserServiceUow
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

