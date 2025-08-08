// File: InvoiceDiscount.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    // A struct to represent the composite key for this table
    public struct InvoiceDiscountKey
    {
        public uint InvoiceId { get; }
        public uint SaleEventId { get; }

        public InvoiceDiscountKey(uint invoiceId, uint saleEventId)
        {
            InvoiceId = invoiceId;
            SaleEventId = saleEventId;
        }
    }

    public class InvoiceDiscount : IGetIdEntity<InvoiceDiscountKey>
    {
        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId { get; private set; }
        public Invoice Invoice { get; private set; } = null!;

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId { get; private set; }
        public SaleEvent SaleEvent { get; private set; } = null!;

        public InvoiceDiscount(uint invoiceId, uint saleEventId)
        {
            InvoiceId = invoiceId;
            SaleEventId = saleEventId;
        }

        public InvoiceDiscountKey GetIdEntity() => new InvoiceDiscountKey(InvoiceId, SaleEventId);
    }
}

