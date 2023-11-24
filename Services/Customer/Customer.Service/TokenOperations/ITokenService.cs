using Customer.Domain.Entity;
using Customer.Domain.Models;

namespace Customer.Service.TokenOperations;

public interface ITokenService
{
    Task<ApiTokenResponse> CreateTokenAsync(ApiUser apiUser);
}
