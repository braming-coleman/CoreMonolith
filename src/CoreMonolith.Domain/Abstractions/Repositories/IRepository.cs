namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
}
