namespace Order.Core.UnifOfWorks;

public class IUnitOfWork
{
    Task SaveChangesAsync();
    void SaveChanges();
}
