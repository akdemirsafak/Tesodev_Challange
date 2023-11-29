namespace Customer.Domain.Models.Address;

public record AddAddressRequest(
    string Line,
    string City,
    string Country,
    int CityCode);