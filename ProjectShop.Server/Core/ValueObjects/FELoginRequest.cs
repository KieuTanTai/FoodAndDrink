using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class FELoginRequest
    {
        // Backing fields
        private readonly string _email = string.Empty;
        private readonly string _password = string.Empty;
        private readonly bool _rememberMe;
        private readonly string? _twoFactorCode;
        private readonly string? _twoFactorRecoveryCode;

        public required string Email
        {
            get => _email;
            init => _email = value ?? string.Empty;
        }

        public required string Password
        {
            get => _password;
            init => _password = value ?? string.Empty;
        }

        public bool RememberMe
        {
            get => _rememberMe;
            init => _rememberMe = value;
        }

        public string? TwoFactorCode
        {
            get => _twoFactorCode;
            init => _twoFactorCode = value;
        }

        public string? TwoFactorRecoveryCode
        {
            get => _twoFactorRecoveryCode;
            init => _twoFactorRecoveryCode = value;
        }
    }
}