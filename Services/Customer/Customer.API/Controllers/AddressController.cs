using Customer.Domain.Models.Address;
using Customer.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.CustomControllerBase;
using Shared.Library.Helper;

namespace Customer.API.Controllers;

[Authorize]
public class AddressController(IAddressService _adressService, ICurrentUser _currentUser) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _adressService.GetAdressAsync(_currentUser.GetUserId));
    }
    [HttpPost("AddAddress")]
    public async Task<IActionResult> AddAddress(AddAddressRequest request)
    {

        return CreateActionResult(await _adressService.AddAdressAsync(request, _currentUser.GetUserId));
    }
    [HttpPost("UpdateAddress")]
    public async Task<IActionResult> UpdateAddress(UpdateAddressRequest request)
    {
        return CreateActionResult(await _adressService.UpdateAdressAsync(request, _currentUser.GetUserId));
    }

}
