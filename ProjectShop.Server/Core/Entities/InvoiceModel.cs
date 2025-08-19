// File: Invoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InvoiceModel : IGetIdEntity<uint>
    {
        // Corresponds to 'invoice_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InvoiceId { get; set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; set; }

        // Corresponds to 'employee_id' (INT UNSIGNED)
        public uint EmployeeId { get; set; }

        // Corresponds to 'payment_method_id' (INT UNSIGNED)
        public uint PaymentMethodId { get; set; }

        // Corresponds to 'invoice_total_price' (DECIMAL(10, 2))
        public decimal InvoiceTotalPrice { get; set; }

        // Corresponds to 'invoice_date' (DATETIME)
        public DateTime InvoiceDate { get; set; }

        // Corresponds to 'invoice_status' (TINYINT(1))
        public bool InvoiceStatus { get; set; }

        // Navigation properties
        public CustomerModel Customer { get; set; } = null!;
        public EmployeeModel Employee { get; set; } = null!;
        public UserPaymentMethodModel UserPaymentMethod { get; set; } = null!;
        public ICollection<DetailInvoiceModel> DetailInvoices { get; set; } = new List<DetailInvoiceModel>();
        public ICollection<InvoiceDiscountModel> InvoiceDiscounts { get; set; } = new List<InvoiceDiscountModel>();
        // End of navigation properties

        public InvoiceModel(uint invoiceId, uint customerId, uint employeeId, uint paymentMethodId, decimal invoiceTotalPrice, DateTime invoiceDate, bool invoiceStatus)
        {
            InvoiceId = invoiceId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            PaymentMethodId = paymentMethodId;
            InvoiceTotalPrice = invoiceTotalPrice;
            InvoiceDate = invoiceDate;
            InvoiceStatus = invoiceStatus;
        }

        public InvoiceModel() { }

        public uint GetIdEntity() => InvoiceId;
    }
}

