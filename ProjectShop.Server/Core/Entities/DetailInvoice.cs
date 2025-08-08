// File: DetailInvoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    // A struct to represent the composite key for this table
    public struct DetailInvoiceKey
    {
        public uint InvoiceId { get; }
        public string ProductBarcode { get; }

        public DetailInvoiceKey(uint invoiceId, string productBarcode)
        {
            InvoiceId = invoiceId;
            ProductBarcode = productBarcode;
        }
    }

    public class DetailInvoice : IGetIdEntity<DetailInvoiceKey>
    {
        // Corresponds to 'detail_invoice_id' (INT UNSIGNED) - This is a simple primary key
        // The SQL file seems to have a typo and lists it as a composite key,
        // but its definition is an INT UNSIGNED primary key.
        public uint DetailInvoiceId { get; private set; }

        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId { get; private set; }
        public Invoice Invoice { get; private set; } = null!;

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }
        public Product Product { get; private set; } = null!;

        // Corresponds to 'detail_invoice_quantity' (INT UNSIGNED)
        public uint DetailInvoiceQuantity { get; private set; }

        // Corresponds to 'detail_invoice_price' (DECIMAL(10, 2))
        public decimal DetailInvoicePrice { get; private set; }

        // Corresponds to 'detail_invoice_status' (TINYINT(1))
        public bool DetailInvoiceStatus { get; private set; }

        public DetailInvoice(uint detailInvoiceId, uint invoiceId, string productBarcode, uint detailInvoiceQuantity, decimal detailInvoicePrice, bool detailInvoiceStatus)
        {
            DetailInvoiceId = detailInvoiceId;
            InvoiceId = invoiceId;
            ProductBarcode = productBarcode;
            DetailInvoiceQuantity = detailInvoiceQuantity;
            DetailInvoicePrice = detailInvoicePrice;
            DetailInvoiceStatus = detailInvoiceStatus;
        }

        // We use the single primary key for this entity
        public DetailInvoiceKey GetIdEntity() => new DetailInvoiceKey(InvoiceId, ProductBarcode);
    }
}

