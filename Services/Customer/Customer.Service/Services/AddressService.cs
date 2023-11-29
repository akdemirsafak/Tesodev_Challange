using Customer.Domain.Entity;
using Customer.Domain.Models.Address;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Shared.Library;

namespace Customer.Service.Services;

public class AddressService(UserManager<ApiUser> _userManager) : IAddressService
{
    public async Task<ApiResponse<NoContent>> AddAdressAsync(AddAddressRequest request, string userId)
    {
        var user= await _userManager.FindByIdAsync(userId);
        var address=request.Adapt<Address>();

        user.Address = address;
        var addedAddressResult=await _userManager.UpdateAsync(user);
        if (!addedAddressResult.Succeeded)
            return ApiResponse<NoContent>.Fail(addedAddressResult.Errors.First().Description, 400);

        return ApiResponse<NoContent>.Success(201);

    }

    public async Task<ApiResponse<GetUserAddressResponse>> GetAdressAsync(string userId)
    {
        var user= await _userManager.FindByIdAsync(userId);
        if (user.Address is null)
            return ApiResponse<GetUserAddressResponse>.Fail("User's address not found.", 404);

        return ApiResponse<GetUserAddressResponse>.Success(user.Address.Adapt<GetUserAddressResponse>());
    }

    public async Task<ApiResponse<NoContent>> UpdateAdressAsync(UpdateAddressRequest request, string userId)
    {
        var user= await _userManager.FindByIdAsync(userId);
        if (user.Address is null)
        {
            var address= request.Adapt<Address>();
            user.Address = address;
        }
        else
        {
            user.Address.Line = request.Line;
            user.Address.City = request.City;
            user.Address.Country = request.Country;
            user.Address.CityCode = request.CityCode;
        }
        var updateResult= await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return ApiResponse<NoContent>.Fail(updateResult.Errors.First().Description, 400);

        return ApiResponse<NoContent>.Success();

    }
}
