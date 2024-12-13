namespace CoreMonolith.Domain.Abstractions.Repositories.Access;

public interface IAccessContainer
{
    public IUserRepository UserRepository { get; }
    public IPermissionRepository PermissionRepository { get; }
    public IUserPermissionRepository UserPermissionRepository { get; }
}
