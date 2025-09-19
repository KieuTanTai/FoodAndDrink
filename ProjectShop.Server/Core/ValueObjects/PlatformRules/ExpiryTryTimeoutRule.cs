using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class ExpiryTryTimeoutRule : BasePlatformRules
    {
        // Backing fields
        private readonly int _maxTryTimes;

        public int MaxTryTimes
        {
            get => _maxTryTimes;
            init => _maxTryTimes = value;
        }

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