using CoreMonolith.Domain.Abstractions.Repositories.Access;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class AccessContainer(
    IUserRepository _userRepo,
    IPermissionRepository _permissionRepo,
    IUserPermissionRepository _userPermissionRepo)
    : IAccessContainer
{
    public IUserRepository UserRepository => _userRepo;
    public IPermissionRepository PermissionRepository => _permissionRepo;
    public IUserPermissionRepository UserPermissionRepository => _userPermissionRepo;
}
