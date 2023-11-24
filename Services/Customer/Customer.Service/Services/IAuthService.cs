using Customer.Domain.Models;
using Customer.Domain.Models.Auth;
using Shared.Library;

namespace Customer.Service.Services;

public interface IAuthService
{
    Task<ApiResponse<ApiTokenResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<ApiTokenResponse>> RegisterAsync(RegisterRequest request);
}
