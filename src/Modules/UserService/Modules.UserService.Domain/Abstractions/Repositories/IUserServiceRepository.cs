using CoreMonolith.Domain.Abstractions.Repositories;

namespace Modules.UserService.Domain.Abstractions.Repositories;

public interface IUserServiceRepository<T> : IRepository<T> where T : class
{
}
