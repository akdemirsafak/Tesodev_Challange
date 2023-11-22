using System.Linq.Expressions;

namespace Order.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> CreateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}
