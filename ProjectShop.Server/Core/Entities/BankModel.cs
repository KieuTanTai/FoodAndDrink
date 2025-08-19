// File: Bank.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class BankModel : IGetIdEntity<uint>
    {
        // Corresponds to 'bank_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint BankId { get; set; }

        // Corresponds to 'bank_name' (NVARCHAR(100))
        public string BankName { get; set; } = string.Empty;

        // Corresponds to 'bank_status' (TINYINT(1))
        public bool BankStatus { get; set; }

        // Navigation properties
        public ICollection<UserPaymentMethodModel> UserPaymentMethods { get; set; } = new List<UserPaymentMethodModel>();
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

