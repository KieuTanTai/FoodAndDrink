using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class DefaultPageSizeRule : BasePlatformRules
    {
        public uint DefaultPageSize { get; init; }

        public DefaultPageSizeRule() : base("", false)
        {
            DefaultPageSize = 0;
        }

        public DefaultPageSizeRule(string type, bool enabled, uint defaultPageSize) : base(type, enabled)
        {
            DefaultPageSize = defaultPageSize;
        }
    }
}
