namespace Order.Model.Requests.Orders;

public record UpdateOrderRequest(
    int Quantity,
    double Price,
    string Status,
    //Guid AdressId,
    Guid ProductId);
