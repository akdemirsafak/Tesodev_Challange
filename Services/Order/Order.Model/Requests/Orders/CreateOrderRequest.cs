namespace Order.Model.Requests.Orders;

public record CreateOrderRequest(
    Guid CustomerId, //CurrentUser yazılınca kaldırılacak
    int Quantity,
    double Price, 
    string Status,
    Guid AdressId, 
    Guid ProductId);
