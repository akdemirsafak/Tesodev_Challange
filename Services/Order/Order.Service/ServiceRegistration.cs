using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Service.Application.Products.Commands;
using Order.Service.Behaviors;
using Order.Service.CronJobs;
using Order.Service.EmailServices;
using Order.Service.Models;
using Shared.Library.Helper;
using System.Reflection;

namespace Order.Service;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProduct.Command)));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddScoped<ICurrentUser, CurrentUser>();

        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        serviceCollection.AddScoped<IEmailService, EmailService>();

        serviceCollection.AddHangfire(x =>
        {
            x.UseSqlServerStorage(configuration.GetConnectionString("Hangfire"));
            RecurringJob.AddOrUpdate<Jobs>(j => j.DailyOrderLogs(),
                Cron.Weekly(DayOfWeek.Saturday,23,59),
                TimeZoneInfo.Utc);           
        });

        serviceCollection.AddHangfireServer();
    }
}
