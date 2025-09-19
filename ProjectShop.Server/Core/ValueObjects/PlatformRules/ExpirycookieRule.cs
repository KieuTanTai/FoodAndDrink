using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class ExpiryCookieRule : BasePlatformRules
    {
        // Backing fields
        private readonly uint _maxAgeDays;

        public uint MaxAgeDays
        {
            get => _maxAgeDays;
            init => _maxAgeDays = value;
        }

        public ExpiryCookieRule() : base("", false)
        {
            MaxAgeDays = 0;
        }

        public ExpiryCookieRule(string type, bool enabled, uint maxAgeDays) : base(type, enabled)
        {
            MaxAgeDays = maxAgeDays;
        }
    }
}
