using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Infrastructure.DbContexts;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.UnitOfWorks;
using System.Reflection;

namespace Order.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(
            opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgresqlDbConnection"),
                    option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(ApiDbContext))!.GetName().Name); });
            });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
