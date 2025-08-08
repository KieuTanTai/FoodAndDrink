// File: Account.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Account : IGetIdEntity<uint>
    {
        // Corresponds to 'account_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint AccountId { get; private set; }

        // Corresponds to 'user_name' (VARCHAR(20) UNIQUE)
        public string UserName { get; private set; }

        // Corresponds to 'password' (VARCHAR(25))
        public string Password { get; private set; }

        // Corresponds to 'account_create_date' (DATETIME)
        public DateTime AccountCreateDate { get; private set; }

        // Corresponds to 'account_status' (TINYINT(1))
        public bool AccountStatus { get; private set; }

        // Navigation properties
        public Customer Customer { get; private set; } = null!;
        public Employee Employee { get; private set; } = null!;
        public ICollection<RolesOfUser> RolesOfUsers { get; private set; } = new List<RolesOfUser>();

        public Account(uint accountId, string userName, string password, DateTime accountCreateDate, bool accountStatus)
        {
            AccountId = accountId;
            UserName = userName;
            Password = password;
            AccountCreateDate = accountCreateDate;
            AccountStatus = accountStatus;
        }

        public uint GetIdEntity() => AccountId;
    }
}

