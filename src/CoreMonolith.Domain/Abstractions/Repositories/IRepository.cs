namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void Remove(T entity);

    void Update(T entity);

    Task<bool> ExistsByIdAsync(Guid Id, CancellationToken cancellationToken = default);

    Task<T?> FindByIdAsync(Guid Id, CancellationToken cancellationToken = default);
}
