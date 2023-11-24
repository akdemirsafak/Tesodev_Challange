using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Customer.Service.Helper;

public class CurrentUser(IHttpContextAccessor _httpContextAccessor) : ICurrentUser
{
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
