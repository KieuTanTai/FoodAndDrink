using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class BasePlatformRules
    {
        // Backing fields
        private readonly string _type = "";
        private readonly bool _enabled = false;

        public string Type
        {
            get => _type;
            init => _type = value ?? string.Empty;
        }

        public bool Enabled
        {
            get => _enabled;
            init => _enabled = value;
        }

        public BasePlatformRules() { }
        public BasePlatformRules(string type, bool enabled)
        {
            Type = type;
            Enabled = enabled;
        }
    }
}