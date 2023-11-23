namespace Order.Model.Requests.Orders;

public record UpdateOrderRequest(
     Guid CustomerId, //CurrentUser yazılınca kaldırılacak
    int Quantity,
    double Price,
    string Status,
    Guid AdressId,
    Guid ProductId);
