using Customer.Domain.Models;
using Customer.Domain.Models.Auth;

namespace Customer.Service.Services;

public interface IAuthService
{
    Task<ApiResponse<ApiTokenResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<ApiTokenResponse>> RegisterAsync(RegisterRequest request);
}
