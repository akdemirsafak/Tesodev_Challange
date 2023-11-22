using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;

namespace Order.Infrastructure.DbContexts;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order.Core.Entities.Order> Orders { get; set; }
    public DbSet<Address> Adresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order.Core.Entities.Order>().Navigation(x=>x.Product).AutoInclude();
        modelBuilder.Entity<Order.Core.Entities.Order>().Navigation(o=>o.Adress).AutoInclude();
        base.OnModelCreating(modelBuilder);
    }

}
