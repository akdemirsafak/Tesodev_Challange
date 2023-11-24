using Customer.Domain.Models;
using Customer.Domain.Models.Address;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customer.Service.Services;

public interface IAdressService
{
    Task<ApiResponse<NoContent>> AddAdressAsync(AddAddressRequest request, string userId);
    Task<ApiResponse<NoContent>> UpdateAdressAsync(UpdateAddressRequest request, string userId);
    Task<ApiResponse<GetUserAddressResponse>> GetAdressAsync(string userId);
}
