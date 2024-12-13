namespace CoreMonolith.Domain.Abstractions.Repositories;

public interface IRepository
{
    void Add<T>(T entity);

    void Remove<T>(T entity);
}
