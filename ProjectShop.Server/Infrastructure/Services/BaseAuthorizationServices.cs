using System.Security.Claims;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Infrastructure.Services
{
    public static class BaseAuthorizationService
    {
        public static ClaimsPrincipal GetCurrentUser(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            return httpContext.User;
        }

        public static string GetCurrentUserName(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ClaimsPrincipal user = GetCurrentUser(httpContext);
            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

    }
}
