namespace Customer.Domain.Models.Address;

public record UpdateAddressRequest(
  string Line,
  string City,
  string Country,
  int CityCode);
