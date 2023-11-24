using Customer.Domain.Models.Auth;
using Customer.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(IAuthService _authService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await _authService.LoginAsync(request));
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return Ok(await _authService.RegisterAsync(request));
    }
}
