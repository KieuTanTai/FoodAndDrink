using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class BasePlatformRules
    {
        public string Type { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public BasePlatformRules() { }
        public BasePlatformRules(string type, bool enabled)
        {
            Type = type;
            Enabled = enabled;
        }
    }
}