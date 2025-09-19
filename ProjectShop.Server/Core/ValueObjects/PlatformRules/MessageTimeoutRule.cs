using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects.PlatformRules
{
    public class MessageTimeoutRule : BasePlatformRules
    {
        // Backing fields
        private readonly uint _maxMessageTimeout;

        public uint MaxMessageTimeout
        {
            get => _maxMessageTimeout;
            init => _maxMessageTimeout = value;
        }

        public MessageTimeoutRule() : base("", false) { }
        public MessageTimeoutRule(string type, bool enabled, uint maxMessageTimeout) : base(type, enabled)
        {
            MaxMessageTimeout = maxMessageTimeout;
        }
    }
}