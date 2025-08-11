// File: Transaction.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class TransactionModel : IGetIdEntity<uint>
    {
        // Corresponds to 'transaction_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint TransactionId { get; private set; }

        // Corresponds to 'point_wallet_id' (INT UNSIGNED)
        public uint PointWalletId { get; private set; }

        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId { get; private set; }

        // Corresponds to 'transaction_date' (DATETIME)
        public DateTime TransactionDate { get; private set; }

        // Corresponds to 'transaction_type' (ENUM)
        public bool TransactionType { get; private set; }

        // Corresponds to 'transaction_current_balance' (DECIMAL(10, 2))
        public decimal TransactionCurrentBalance { get; private set; }

        // Corresponds to 'transaction_status' (TINYINT(1))
        public bool TransactionStatus { get; private set; }

        // Navigation properties
        public PointWalletModel PointWallet { get; private set; } = null!;
        public InvoiceModel Invoice { get; private set; } = null!;

        public TransactionModel(uint transactionId, uint pointWalletId, uint invoiceId, DateTime transactionDate, bool transactionType, decimal transactionCurrentBalance, bool transactionStatus)
        {
            TransactionId = transactionId;
            PointWalletId = pointWalletId;
            InvoiceId = invoiceId;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            TransactionCurrentBalance = transactionCurrentBalance;
            TransactionStatus = transactionStatus;
        }

        public uint GetIdEntity() => TransactionId;
    }
}
