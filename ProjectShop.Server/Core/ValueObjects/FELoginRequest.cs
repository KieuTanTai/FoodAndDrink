using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class FELoginRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
        public bool RememberMe { get; init; }
        public string? TwoFactorCode { get; init; }
        public string? TwoFactorRecoveryCode { get; init; }
    }
}