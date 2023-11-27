using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Library.Helper;

public class CurrentUser(IHttpContextAccessor _httpContextAccessor) : ICurrentUser
{
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
