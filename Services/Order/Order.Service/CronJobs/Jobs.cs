using Order.Core.Repositories;
using Order.Service.EmailServices;
using Order.Service.Models;

namespace Order.Service.CronJobs;

public class Jobs
{
    private readonly IGenericRepository<Order.Core.Entities.Order> _orderRepository;
    private readonly IEmailService _emailService;
    public Jobs(IGenericRepository<Core.Entities.Order> orderRepository, IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
    }

    public async Task DailyOrderLogs()
    {
        var logs= await _orderRepository.GetAllAsync(x=>x.CreatedAt.Ticks == DateTime.UtcNow.Ticks);
        var sendDailyOrderLogsModel= new SendDailyOrderLogsModel("admin@domain.com","Daily order logs",logs);
        await _emailService.SendDailyOrderLogs(sendDailyOrderLogsModel); 
    }
}
