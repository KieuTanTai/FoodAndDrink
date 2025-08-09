// File: UserPaymentMethod.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class UserPaymentMethodModel : IGetIdEntity<uint>
    {
        // Corresponds to 'user_payment_method_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint UserPaymentMethodId { get; private set; }

        // Corresponds to 'payment_method_type' (ENUM)
        public string PaymentMethodType { get; private set; }

        // Corresponds to 'bank_id' (INT UNSIGNED)
        public uint? BankId { get; private set; }

        // Corresponds to 'account_id' (INT UNSIGNED)
        public uint CustomerId { get; private set; }

        // Corresponds to 'added_date' (DATETIME)
        public DateTime AddedDate { get; private set; }

        // Corresponds to 'last_updated_date' (DATETIME)
        public DateTime LastUpdatedDate { get; private set; }

        // Corresponds to 'status' (ENUM)
        public string Status { get; private set; }

        // Corresponds to 'display_name' (VARCHAR(50))
        public string DisplayName { get; private set; }

        // Corresponds to 'last_four_digit' (VARCHAR(4))
        public string LastFourDigit { get; private set; }

        // Corresponds to 'expiry_year' (INT)
        public int ExpiryYear { get; private set; }

        // Corresponds to 'expiry_month' (INT)
        public int ExpiryMonth { get; private set; }

        // Corresponds to 'token' (VARCHAR(255))
        public string Token { get; private set; }

        // Navigation property
        public BankModel? Bank { get; private set; }
        public CustomerModel Customer { get; private set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; private set; } = new List<InvoiceModel>();

        public UserPaymentMethodModel(uint userPaymentMethodId, string paymentMethodType, uint? bankId, uint customerId, DateTime addedDate, DateTime lastUpdatedDate, string status, string displayName, string lastFourDigit, int expiryYear, int expiryMonth, string token)
        {
            UserPaymentMethodId = userPaymentMethodId;
            PaymentMethodType = paymentMethodType;
            BankId = bankId;
            CustomerId = customerId;
            AddedDate = addedDate;
            LastUpdatedDate = lastUpdatedDate;
            Status = status;
            DisplayName = displayName;
            LastFourDigit = lastFourDigit;
            ExpiryYear = expiryYear;
            ExpiryMonth = expiryMonth;
            Token = token;
        }

        public uint GetIdEntity() => UserPaymentMethodId;
    }
}

