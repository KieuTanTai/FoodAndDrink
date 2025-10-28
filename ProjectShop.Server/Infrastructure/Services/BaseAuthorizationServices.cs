using System.Security.Claims;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Infrastructure.Services
{
    public static class BaseAuthorizationService
    {
        public static ClaimsPrincipal GetClaimsPrincipalByIdAsync(uint userId)
        {
            if (userId == 0)
                throw new ArgumentException("UserId cannot be zero.", nameof(userId));
            if (ClaimsPrincipal.Current == null)
                throw new InvalidOperationException("No current ClaimsPrincipal is available.");
            return ClaimsPrincipal.Current;
        }
    }
}
