using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class ExpiryTryTimeoutRule : BasePlatformRules
    {
        public int MaxTryTimes { get; init; }

        public ExpiryTryTimeoutRule() : base("", false)
        {
            MaxTryTimes = 0;
        }

        public ExpiryTryTimeoutRule(string type, bool enabled, int maxTryTimes) : base(type, enabled)
        {
            MaxTryTimes = maxTryTimes;
        }
    }
}