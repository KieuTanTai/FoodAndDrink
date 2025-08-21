// File: UserPaymentMethod.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class UserPaymentMethodModel : IGetIdEntity<uint>
    {
        // Corresponds to 'user_payment_method_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint UserPaymentMethodId { get; set; }

        // Corresponds to 'payment_method_type' (ENUM)
        public string PaymentMethodType { get; set; } = string.Empty;

        // Corresponds to 'bank_id' (INT UNSIGNED)
        public uint? BankId { get; set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; set; }

        // Corresponds to 'payment_method_added_date' (DATETIME)
        public DateTime PaymentMethodAddedDate { get; set; }

        // Corresponds to 'payment_method_last_updated_date' (DATETIME)
        public DateTime PaymentMethodLastUpdatedDate { get; set; }

        // Corresponds to 'payment_method_status' (TINYINT)
        public int PaymentMethodStatus { get; set; }

        // Corresponds to 'payment_method_display_name' (VARCHAR(50))
        public string PaymentMethodDisplayName { get; set; } = string.Empty;

        // Corresponds to 'payment_method_last_four_digit' (VARCHAR(4))
        public string PaymentMethodLastFourDigit { get; set; } = string.Empty;

        // Corresponds to 'payment_method_expiry_year' (INT)
        public int PaymentMethodExpiryYear { get; set; }

        // Corresponds to 'payment_method_expiry_month' (INT)
        public int PaymentMethod_ExpiryMonth { get; set; }

        // Corresponds to 'payment_method_token' (VARCHAR(255))
        public string PaymentMethodToken { get; set; } = string.Empty;

        // Navigation properties
        public BankModel? Bank { get; set; }
        public CustomerModel Customer { get; set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
        // End of navigation properties

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

        public UserPaymentMethodModel()
        {
        }

        public uint GetIdEntity() => UserPaymentMethodId;
    }
}

