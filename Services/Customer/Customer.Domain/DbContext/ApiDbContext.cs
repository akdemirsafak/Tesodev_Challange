using Customer.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Customer.Domain.DbContext;

public class ApiDbContext : IdentityDbContext<ApiUser, IdentityRole, string>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApiUser>().Navigation(x => x.Address).AutoInclude();
        base.OnModelCreating(builder);
    }
}
