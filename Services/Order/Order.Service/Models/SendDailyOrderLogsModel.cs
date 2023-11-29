namespace Order.Service.Models;

public record SendDailyOrderLogsModel(string To,string subject,List<Order.Core.Entities.Order> logs);
