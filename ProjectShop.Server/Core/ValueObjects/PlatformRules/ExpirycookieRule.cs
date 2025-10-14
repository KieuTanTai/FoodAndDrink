using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class ExpiryCookieRule : BasePlatformRules
    {
        public uint MaxAgeDays { get; init; }

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
