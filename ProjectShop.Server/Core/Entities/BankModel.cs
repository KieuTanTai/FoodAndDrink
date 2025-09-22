// File: Bank.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class BankModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _bankId;
        private string _bankName = string.Empty;
        private bool _bankStatus;

        // Corresponds to 'bank_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint BankId
        {
            get => _bankId;
            set => _bankId = value;
        }

        // Corresponds to 'bank_name' (NVARCHAR(100))
        public string BankName
        {
            get => _bankName;
            set => _bankName = value ?? string.Empty;
        }

        // Corresponds to 'bank_status' (TINYINT(1))
        public bool BankStatus
        {
            get => _bankStatus;
            set => _bankStatus = value;
        }

        // Navigation properties
        public ICollection<UserPaymentMethodModel> UserPaymentMethods { get; set; } = [];
        // End of navigation properties

        public BankModel(uint bankId, string bankName, bool bankStatus)
        {
            BankId = bankId;
            BankName = bankName;
            BankStatus = bankStatus;
        }

        public BankModel()
        {
        }

        public uint GetIdEntity() => BankId;
    }
}

