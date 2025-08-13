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

        // Corresponds to 'payment_method_added_date' (DATETIME)
        public DateTime PaymentMethodAddedDate { get; private set; }

        // Corresponds to 'payment_method_last_updated_date' (DATETIME)
        public DateTime PaymentMethodLastUpdatedDate { get; private set; }

        // Corresponds to 'payment_method_status' (TINYINT)
        public int PaymentMethodStatus { get; private set; }

        // Corresponds to 'payment_method_display_name' (VARCHAR(50))
        public string PaymentMethodDisplayName { get; private set; }

        // Corresponds to 'payment_method_last_four_digit' (VARCHAR(4))
        public string PaymentMethodLastFourDigit { get; private set; }

        // Corresponds to 'payment_method_expiry_year' (INT)
        public int PaymentMethodExpiryYear { get; private set; }

        // Corresponds to 'payment_method_expiry_month' (INT)
        public int PaymentMethod_ExpiryMonth { get; private set; }

        // Corresponds to 'payment_method_token' (VARCHAR(255))
        public string PaymentMethodToken { get; private set; }

        // Navigation property
        public BankModel? Bank { get; private set; }
        public CustomerModel Customer { get; private set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; private set; } = new List<InvoiceModel>();

        public UserPaymentMethodModel(uint userPaymentMethodId, string paymentMethodType, uint? bankId, uint customerId, DateTime paymentMethodAddedDate, DateTime paymentMethodLastUpdatedDate, int paymentMethodStatus,
            string paymentMethodDisplayName, string paymentMethodLastFourDigit, int paymentMethodExpiryYear, int paymentMethod_ExpiryMonth, string paymentMethodToken)
        {
            UserPaymentMethodId = userPaymentMethodId;
            PaymentMethodType = paymentMethodType;
            BankId = bankId;
            CustomerId = customerId;
            PaymentMethodAddedDate = paymentMethodAddedDate;
            PaymentMethodLastUpdatedDate = paymentMethodLastUpdatedDate;
            PaymentMethodStatus = paymentMethodStatus;
            PaymentMethodDisplayName = paymentMethodDisplayName;
            PaymentMethodLastFourDigit = paymentMethodLastFourDigit;
            PaymentMethodExpiryYear = paymentMethodExpiryYear;
            PaymentMethod_ExpiryMonth = paymentMethod_ExpiryMonth;
            PaymentMethodToken = paymentMethodToken;
        }

        public uint GetIdEntity() => UserPaymentMethodId;
    }
}

