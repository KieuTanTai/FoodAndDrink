// File: Invoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InvoiceModel : IGetIdEntity<uint>
    {
        // Corresponds to 'invoice_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InvoiceId { get; private set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; private set; }

        // Corresponds to 'payment_method_id' (INT UNSIGNED)
        public uint PaymentMethodId { get; private set; }

        // Corresponds to 'invoice_total_price' (DECIMAL(10, 2))
        public decimal InvoiceTotalPrice { get; private set; }

        // Corresponds to 'invoice_date' (DATETIME)
        public DateTime InvoiceDate { get; private set; }

        // Corresponds to 'invoice_status' (TINYINT(1))
        public bool InvoiceStatus { get; private set; }

        // Navigation properties
        public CustomerModel Customer { get; private set; } = null!;
        public UserPaymentMethodModel UserPaymentMethod { get; private set; } = null!;
        public ICollection<DetailInvoiceModel> DetailInvoices { get; private set; } = new List<DetailInvoiceModel>();
        public ICollection<InvoiceDiscount> InvoiceDiscounts { get; private set; } = new List<InvoiceDiscount>();
        public ICollection<TransactionModel> Transactions { get; private set; } = new List<TransactionModel>();

        public InvoiceModel(uint invoiceId, uint customerId, uint paymentMethodId, decimal invoiceTotalPrice, DateTime invoiceDate, bool invoiceStatus)
        {
            InvoiceId = invoiceId;
            CustomerId = customerId;
            PaymentMethodId = paymentMethodId;
            InvoiceTotalPrice = invoiceTotalPrice;
            InvoiceDate = invoiceDate;
            InvoiceStatus = invoiceStatus;
        }

        public uint GetIdEntity() => InvoiceId;
    }
}

