// File: Invoice.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InvoiceModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _invoiceId;
        private uint _customerId;
        private uint _employeeId;
        private uint _paymentMethodId;
        private decimal _invoiceTotalPrice;
        private DateTime _invoiceDate;
        private bool _invoiceStatus;
        private EInvoicePaymentType _paymentType = EInvoicePaymentType.COD;

        // Corresponds to 'invoice_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InvoiceId
        {
            get => _invoiceId;
            set => _invoiceId = value;
        }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }

        // Corresponds to 'employee_id' (INT UNSIGNED)
        public uint EmployeeId
        {
            get => _employeeId;
            set => _employeeId = value;
        }

        // Corresponds to 'payment_method_id' (INT UNSIGNED)
        public uint PaymentMethodId
        {
            get => _paymentMethodId;
            set => _paymentMethodId = value;
        }

        // Corresponds to 'invoice_total_price' (DECIMAL(10, 2))
        public decimal InvoiceTotalPrice
        {
            get => _invoiceTotalPrice;
            set => _invoiceTotalPrice = value;
        }

        // Corresponds to 'invoice_date' (DATETIME)
        public DateTime InvoiceDate
        {
            get => _invoiceDate;
            set => _invoiceDate = value;
        }

        // Corresponds to 'invoice_status' (TINYINT(1))
        public bool InvoiceStatus
        {
            get => _invoiceStatus;
            set => _invoiceStatus = value;
        }

        // Corresponds to 'payment_type' (ENUM)
        public EInvoicePaymentType PaymentType
        {
            get => _paymentType;
            set => _paymentType = value;
        }

        // Navigation properties
        public CustomerModel Customer { get; set; } = null!;
        public EmployeeModel Employee { get; set; } = null!;
        public UserPaymentMethodModel UserPaymentMethod { get; set; } = null!;
        public ICollection<DetailInvoiceModel> DetailInvoices { get; set; } = new List<DetailInvoiceModel>();
        public ICollection<InvoiceDiscountModel> InvoiceDiscounts { get; set; } = new List<InvoiceDiscountModel>();
        // End of navigation properties

        public InvoiceModel(uint invoiceId, uint customerId, uint employeeId, uint paymentMethodId, decimal invoiceTotalPrice,
            DateTime invoiceDate, bool invoiceStatus, EInvoicePaymentType paymentType)
        {
            InvoiceId = invoiceId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            PaymentMethodId = paymentMethodId;
            InvoiceTotalPrice = invoiceTotalPrice;
            InvoiceDate = invoiceDate;
            InvoiceStatus = invoiceStatus;
            PaymentType = paymentType;
        }

        public InvoiceModel() { }

        public uint GetIdEntity() => InvoiceId;
    }
}

