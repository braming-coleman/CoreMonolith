using CoreMonolith.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Domain.Abstractions.Repositories;

public interface IDownloadServiceRepository<T> : IRepository<T> where T : class
{
}
