using Customer.Domain.Models.Address;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Library;

namespace Customer.Service.Services;

public interface IAddressService
{
    Task<ApiResponse<NoContent>> AddAdressAsync(AddAddressRequest request, string userId);
    Task<ApiResponse<NoContent>> UpdateAdressAsync(UpdateAddressRequest request, string userId);
    Task<ApiResponse<GetUserAddressResponse>> GetAdressAsync(string userId);
}
