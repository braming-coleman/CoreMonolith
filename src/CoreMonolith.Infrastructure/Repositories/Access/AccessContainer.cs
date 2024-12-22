using CoreMonolith.Domain.Abstractions.Repositories.Access;

namespace CoreMonolith.Infrastructure.Repositories.Access;

public class AccessContainer(
    IUserRepository _userRepo,
    IPermissionRepository _permissionRepo,
    IPermissionGroupRepository _permissionGroupRepo,
    IUserPermissionGroupRepository _userPermissionGroupRepo)
    : IAccessContainer
{
    public IUserRepository UserRepository => _userRepo;
    public IPermissionRepository PermissionRepository => _permissionRepo;
    public IPermissionGroupRepository PermissionGroupRepository => _permissionGroupRepo;
    public IUserPermissionGroupRepository UserPermissionGroupRepository => _userPermissionGroupRepo;
}
