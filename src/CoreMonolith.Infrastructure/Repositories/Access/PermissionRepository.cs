using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class PermissionRepository(
    ApplicationDbContext _dbContext)
    : Repository<Permission>(_dbContext), IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext = _dbContext;
}
