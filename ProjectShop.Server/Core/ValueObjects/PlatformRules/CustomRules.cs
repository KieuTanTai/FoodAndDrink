using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class CustomRules
    {
        // Backing fields
        private readonly ExpiryCookieRule _expiryCookieRule = new();
        private readonly ExpiryFetchTimeoutRule _expiryFetchTimeoutRule = new();
        private readonly ExpiryTryTimeoutRule _expiryTryTimeoutRule = new();
        private readonly MessageTimeoutRule _messageTimeoutRule = new();

        public ExpiryCookieRule ExpiryCookieRule
        {
            get => _expiryCookieRule;
            init => _expiryCookieRule = value ?? new();
        }

        public ExpiryFetchTimeoutRule ExpiryFetchTimeoutRule
        {
            get => _expiryFetchTimeoutRule;
            init => _expiryFetchTimeoutRule = value ?? new();
        }

        public ExpiryTryTimeoutRule ExpiryTryTimeoutRule
        {
            get => _expiryTryTimeoutRule;
            init => _expiryTryTimeoutRule = value ?? new();
        }

        public MessageTimeoutRule MessageTimeoutRule
        {
            get => _messageTimeoutRule;
            init => _messageTimeoutRule = value ?? new();
        }

        public CustomRules() { }

        public CustomRules(IDictionary<string, IConfigurationSection> keyValuePairs)
        {

        }

        public CustomRules(ExpiryCookieRule expiryCookieRule, ExpiryFetchTimeoutRule expiryFetchTimeoutRule,
                            ExpiryTryTimeoutRule expiryTryTimeoutRule, MessageTimeoutRule messageTimeoutRule)
        {
            ExpiryCookieRule = expiryCookieRule;
            ExpiryFetchTimeoutRule = expiryFetchTimeoutRule;
            ExpiryTryTimeoutRule = expiryTryTimeoutRule;
            MessageTimeoutRule = messageTimeoutRule;
        }

    }
}