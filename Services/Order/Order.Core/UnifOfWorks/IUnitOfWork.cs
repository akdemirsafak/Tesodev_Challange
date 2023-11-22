namespace Order.Core.UnifOfWorks;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    void SaveChanges();
}
