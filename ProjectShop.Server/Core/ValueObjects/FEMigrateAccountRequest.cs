using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class FEMigrateAccountRequest
    {
        // Backing fields
        private ICollection<AccountModel> _accounts = [];
        private string _adminKey = string.Empty;

        public ICollection<AccountModel> Accounts
        {
            get => _accounts;
            set => _accounts = value ?? [];
        }

        // key for admin to authorize migration
        public string AdminKey
        {
            get => _adminKey;
            set => _adminKey = value ?? string.Empty;
        }

        public FEMigrateAccountRequest()
        {
        }

        public FEMigrateAccountRequest(ICollection<AccountModel> accounts, string adminKey)
        {
            Accounts = accounts;
            AdminKey = adminKey;
        }
    }
}