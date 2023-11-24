using Customer.Domain.Models.Address;
using Customer.Service.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(ICurrentUser _currentUser) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress(AddAddressRequest request)
        {
            return Ok();
        }
        
    }
}
