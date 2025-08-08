// File: Invoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Invoice : IGetIdEntity<uint>
    {
        // Corresponds to 'invoice_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InvoiceId { get; private set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        // Corresponds to 'payment_method_id' (INT UNSIGNED)
        public uint PaymentMethodId { get; private set; }
        public UserPaymentMethod UserPaymentMethod { get; private set; } = null!;

        // Corresponds to 'invoice_total_price' (DECIMAL(10, 2))
        public decimal InvoiceTotalPrice { get; private set; }

        // Corresponds to 'invoice_date' (DATETIME)
        public DateTime InvoiceDate { get; private set; }

        // Corresponds to 'invoice_status' (TINYINT(1))
        public bool InvoiceStatus { get; private set; }

        // Navigation properties
        public ICollection<DetailInvoice> DetailInvoices { get; private set; } = new List<DetailInvoice>();
        public ICollection<InvoiceDiscount> InvoiceDiscounts { get; private set; } = new List<InvoiceDiscount>();
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public Invoice(uint invoiceId, uint customerId, uint paymentMethodId, decimal invoiceTotalPrice, DateTime invoiceDate, bool invoiceStatus)
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

