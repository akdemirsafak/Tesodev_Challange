using Customer.Domain.Models.Auth;
using Customer.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.CustomControllerBase;

namespace Customer.API.Controllers;

public class CustomerController(IAuthService _authService) : CustomBaseController
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return CreateActionResult(await _authService.LoginAsync(request));
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return CreateActionResult(await _authService.RegisterAsync(request));
    }
}
