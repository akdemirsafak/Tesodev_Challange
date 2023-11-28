using Customer.Domain.Entity;
using Customer.Domain.Models;
using Customer.Domain.Models.Auth;
using Customer.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;
using Shared.Library;

namespace Customer.Service.Services;

public class AuthService(UserManager<ApiUser> _userManager, ITokenService _tokenService) : IAuthService
{
    public async Task<ApiResponse<ApiTokenResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return ApiResponse<ApiTokenResponse>.Fail("User not found.", 404);
        var passwordIsCorrect= await _userManager.CheckPasswordAsync(user,request.Password);
        if (!passwordIsCorrect)
        {
            return ApiResponse<ApiTokenResponse>.Fail("InCorrect password.", 400);
        }

        return ApiResponse<ApiTokenResponse>.Success(await _tokenService.CreateTokenAsync(user), 200);
    }

    public async Task<ApiResponse<ApiTokenResponse>> RegisterAsync(RegisterRequest request)
    {
        var user= new ApiUser
        {
            UserName=Guid.NewGuid().ToString(),
            Name=request.Name,
            Email=request.Email
        };
        var createResult=await _userManager.CreateAsync(user,request.Password);
        if (!createResult.Succeeded)
        {
            return ApiResponse<ApiTokenResponse>.Fail(createResult.Errors.First().Description, 500);
        }
        var token =await _tokenService.CreateTokenAsync(user);
        return ApiResponse<ApiTokenResponse>.Success(token);
    }
}
