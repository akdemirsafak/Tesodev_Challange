namespace Order.Core.Entities;

public class Order
{
    public Order()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
    public virtual Address Adress { get; set; }
    public virtual Product Product { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
