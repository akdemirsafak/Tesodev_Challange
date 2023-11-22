using Order.Core.UnifOfWorks;
using Order.Infrastructure.DbContexts;

namespace Order.Infrastructure.UnitOfWorks;

public class UnitOfWork(ApiDbContext _dbContext) : IUnitOfWork
{
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
