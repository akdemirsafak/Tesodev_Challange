using Microsoft.EntityFrameworkCore;
using Order.Core.Repositories;
using Order.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApiDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
    
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var addedEntity=await _dbSet.AddAsync(entity);
            addedEntity.State=EntityState.Added;
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await(filter == null ?
                   _dbSet.ToListAsync() :
                   _dbSet.Where(filter).ToListAsync());
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var deletedEntity = _dbSet.Remove(entity);
            deletedEntity.State = EntityState.Deleted;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updatedEntity = _dbSet.Update(entity);
            updatedEntity.State = EntityState.Modified;
        }
    }
}
