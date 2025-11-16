using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class RateLimitingRule : BasePlatformRules
    {
        public uint MaxRequestsPerMinute { get; init; }

        public RateLimitingRule() : base("", false)
        {
            MaxRequestsPerMinute = 0;
        }

        public RateLimitingRule(string type, bool enabled, uint maxRequestsPerMinute) : base(type, enabled)
        {
            MaxRequestsPerMinute = maxRequestsPerMinute;
        }
    }
}
