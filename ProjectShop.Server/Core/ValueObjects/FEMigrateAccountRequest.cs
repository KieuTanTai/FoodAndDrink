using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.ValueObjects
{
    public class FEMigrateAccountRequest
    {
        public ICollection<Account> Accounts { get; set; } = [];

        // key for admin to authorize migration
        public string AdminKey { get; set; } = string.Empty;

        public FEMigrateAccountRequest()
        {
        }

        public FEMigrateAccountRequest(ICollection<Account> accounts, string adminKey)
        {
            Accounts = accounts;
            AdminKey = adminKey;
        }
    }
}