using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class AccountUpdateRelativeRequest
    {
        // Backing fields
        private uint _accountId;
        private bool _isUpdateCustomer = false;
        private bool _isUpdateEmployee = false;

        public uint AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        public bool IsUpdateCustomer
        {
            get => _isUpdateCustomer;
            set => _isUpdateCustomer = value;
        }

        public bool IsUpdateEmployee
        {
            get => _isUpdateEmployee;
            set => _isUpdateEmployee = value;
        }
    }
}