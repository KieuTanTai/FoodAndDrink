using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class AccountUpdateRelativeRequest
    {
        public uint AccountId { get; set; }
        public bool IsUpdateCustomer { get; set; } = false;
        public bool IsUpdateEmployee { get; set; } = false;
    }
}