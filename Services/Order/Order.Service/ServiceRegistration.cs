using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Service.Application.Products.Commands;
using Order.Service.Behaviors;
using System.Reflection;

namespace Order.Service;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProduct.Command)));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
