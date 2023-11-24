using Order.Model.Responses.Product;

namespace Order.Model.Responses.Orders;

public class CreatedOrderResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
    public string Adress { get; set; }
    public GetProductResponse Product { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
