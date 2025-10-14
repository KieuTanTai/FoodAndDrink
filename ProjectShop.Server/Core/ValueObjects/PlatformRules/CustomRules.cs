using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class CustomRules
    {
        public ExpiryCookieRule ExpiryCookieRule { get; init; } = new();
        public ExpiryFetchTimeoutRule ExpiryFetchTimeoutRule { get; init; } = new();
        public ExpiryTryTimeoutRule ExpiryTryTimeoutRule { get; init; } = new();
        public MessageTimeoutRule MessageTimeoutRule { get; init; } = new();

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