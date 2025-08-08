// File: PointWallet.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class PointWallet : IGetIdEntity<uint>
    {
        // Corresponds to 'point_wallet_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint PointWalletId { get; private set; }

        // Corresponds to 'customer_id' (INT UNSIGNED UNIQUE)
        public uint CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        // Corresponds to 'balance_point' (DECIMAL(10, 2))
        public decimal BalancePoint { get; private set; }

        // Corresponds to 'last_update_balance_date' (DATETIME)
        public DateTime LastUpdateBalanceDate { get; private set; }

        // Navigation property
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public PointWallet(uint pointWalletId, uint customerId, decimal balancePoint, DateTime lastUpdateBalanceDate)
        {
            PointWalletId = pointWalletId;
            CustomerId = customerId;
            BalancePoint = balancePoint;
            LastUpdateBalanceDate = lastUpdateBalanceDate;
        }

        public uint GetIdEntity() => PointWalletId;
    }
}

