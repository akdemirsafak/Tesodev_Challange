namespace Order.Core.Entities;

public class Address
{
    public Guid Id { get; set; } //Geçici
    public string Line { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int CityCode { get; set; }
}
