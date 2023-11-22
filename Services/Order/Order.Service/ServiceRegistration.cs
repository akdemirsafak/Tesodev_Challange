using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Order.Service.Application.Products.Commands;
using System.Reflection;

namespace Order.Service;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProduct.Command)));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
