using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.FrontEndRequestsForAccount
{
    public class FELoginRequest
    {
        public required string Email { get; init; } = string.Empty;
        public required string Password { get; init; } = string.Empty;
        public bool RememberMe { get; init; }
        public string? TwoFactorCode { get; init; }
        public string? TwoFactorRecoveryCode { get; init; }
    }
}