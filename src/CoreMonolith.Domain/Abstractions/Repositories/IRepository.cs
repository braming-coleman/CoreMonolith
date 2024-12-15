namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken);

    void Remove(T entity);

    void Update(T entity);

    Task<bool> ExistsByIdAsync(Guid Id, CancellationToken cancellationToken);

    Task<T?> FindByIdAsync(Guid Id, CancellationToken cancellationToken);
}
