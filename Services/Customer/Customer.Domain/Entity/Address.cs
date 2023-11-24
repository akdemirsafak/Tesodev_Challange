namespace Customer.Domain.Entity;

public class Address
{
    public Guid Id { get; set; }
    public string Line { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int CityCode { get; set; }
    public virtual List<ApiUser> Users { get; set; }
}
