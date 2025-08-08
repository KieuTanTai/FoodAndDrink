// File: Bank.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Bank : IGetIdEntity<uint>
    {
        // Corresponds to 'bank_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint BankId { get; private set; }

        // Corresponds to 'bank_name' (NVARCHAR(100))
        public string BankName { get; private set; }

        // Corresponds to 'bank_status' (TINYINT(1))
        public bool BankStatus { get; private set; }

        // Navigation property
        public ICollection<UserPaymentMethod> UserPaymentMethods { get; private set; } = new List<UserPaymentMethod>();

        public Bank(uint bankId, string bankName, bool bankStatus)
        {
            BankId = bankId;
            BankName = bankName;
            BankStatus = bankStatus;
        }

        public uint GetIdEntity() => BankId;
    }
}

