using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class PermissionRepository(
    ApplicationDbContext _dbContext,
    IUnitOfWork _unitOfWork)
    : Repository<Permission>(_dbContext), IPermissionRepository
{
}
