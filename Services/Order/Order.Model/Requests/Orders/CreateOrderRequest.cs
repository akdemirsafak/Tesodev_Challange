namespace Order.Model.Requests.Orders;

public record CreateOrderRequest(
    int Quantity,
    double Price,
    string Status,
    Guid AdressId, //RabitMq'dan sonra bakalım.
    Guid ProductId);
