// File: UserPaymentMethod.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class UserPaymentMethodModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _userPaymentMethodId;
        private string _paymentMethodType = string.Empty;
        private uint? _bankId;
        private uint _customerId;
        private DateTime _paymentMethodAddedDate;
        private DateTime _paymentMethodLastUpdatedDate;
        private int _paymentMethodStatus;
        private string _paymentMethodDisplayName = string.Empty;
        private string _paymentMethodLastFourDigit = string.Empty;
        private int _paymentMethodExpiryYear;
        private int _paymentMethodExpiryMonth;
        private string _paymentMethodToken = string.Empty;

        // Corresponds to 'user_payment_method_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint UserPaymentMethodId
        {
            get => _userPaymentMethodId;
            set => _userPaymentMethodId = value;
        }

        // Corresponds to 'payment_method_type' (ENUM)
        public string PaymentMethodType
        {
            get => _paymentMethodType;
            set => _paymentMethodType = value ?? string.Empty;
        }

        // Corresponds to 'bank_id' (INT UNSIGNED)
        public uint? BankId
        {
            get => _bankId;
            set => _bankId = value;
        }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }

        // Corresponds to 'payment_method_added_date' (DATETIME)
        public DateTime PaymentMethodAddedDate
        {
            get => _paymentMethodAddedDate;
            set => _paymentMethodAddedDate = value;
        }

        // Corresponds to 'payment_method_last_updated_date' (DATETIME)
        public DateTime PaymentMethodLastUpdatedDate
        {
            get => _paymentMethodLastUpdatedDate;
            set => _paymentMethodLastUpdatedDate = value;
        }

        // Corresponds to 'payment_method_status' (TINYINT)
        public int PaymentMethodStatus
        {
            get => _paymentMethodStatus;
            set => _paymentMethodStatus = value;
        }

        // Corresponds to 'payment_method_display_name' (VARCHAR(50))
        public string PaymentMethodDisplayName
        {
            get => _paymentMethodDisplayName;
            set => _paymentMethodDisplayName = value ?? string.Empty;
        }

        // Corresponds to 'payment_method_last_four_digit' (VARCHAR(4))
        public string PaymentMethodLastFourDigit
        {
            get => _paymentMethodLastFourDigit;
            set => _paymentMethodLastFourDigit = value ?? string.Empty;
        }

        // Corresponds to 'payment_method_expiry_year' (INT)
        public int PaymentMethodExpiryYear
        {
            get => _paymentMethodExpiryYear;
            set => _paymentMethodExpiryYear = value;
        }

        // Corresponds to 'payment_method_expiry_month' (INT)
        public int PaymentMethod_ExpiryMonth
        {
            get => _paymentMethodExpiryMonth;
            set => _paymentMethodExpiryMonth = value;
        }

        // Corresponds to 'payment_method_token' (VARCHAR(255))
        public string PaymentMethodToken
        {
            get => _paymentMethodToken;
            set => _paymentMethodToken = value ?? string.Empty;
        }

        // Navigation properties
        public BankModel? Bank { get; set; }
        public CustomerModel Customer { get; set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; set; } = [];
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

