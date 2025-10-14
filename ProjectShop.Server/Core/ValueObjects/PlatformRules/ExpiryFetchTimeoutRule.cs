using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class ExpiryFetchTimeoutRule : BasePlatformRules
    {
        public uint MaxFetchTimeout { get; init; }

        public ExpiryFetchTimeoutRule(string type, bool enabled, uint maxFetchTimeout) : base(type, enabled)
        {
            MaxFetchTimeout = maxFetchTimeout;
        }

        public ExpiryFetchTimeoutRule() : base("", false)
        {
            MaxFetchTimeout = 0;
        }
    }
}