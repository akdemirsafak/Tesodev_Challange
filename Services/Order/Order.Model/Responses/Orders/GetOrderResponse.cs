using Order.Model.Responses.Product;

namespace Order.Model.Responses.Orders;

public class GetOrderResponse
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
    public string Adress { get; set; }
    public GetProductResponse Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
