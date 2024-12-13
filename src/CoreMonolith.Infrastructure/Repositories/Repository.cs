using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Infrastructure.Repositories;

public class Repository<T>(
    ApplicationDbContext _dbContext)
    : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = _dbContext.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
