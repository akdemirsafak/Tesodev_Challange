using Order.Service.Models;

namespace Order.Service.EmailServices;

public interface IEmailService
{
    Task SendDailyOrderLogs(SendDailyOrderLogsModel sendDailyOrderLogsModel);
}
