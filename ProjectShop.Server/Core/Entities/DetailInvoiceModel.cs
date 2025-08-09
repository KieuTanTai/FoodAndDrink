// File: DetailInvoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInvoiceModel : IGetIdEntity<uint>
    {
        public uint DetailInvoiceId { get; private set; }

        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }

        // Corresponds to 'detail_invoice_quantity' (INT UNSIGNED)
        public uint DetailInvoiceQuantity { get; private set; }

        // Corresponds to 'detail_invoice_price' (DECIMAL(10, 2))
        public decimal DetailInvoicePrice { get; private set; }

        // Corresponds to 'detail_invoice_status' (TINYINT(1))
        public bool DetailInvoiceStatus { get; private set; }

        // Navigation property
        public InvoiceModel Invoice { get; private set; } = null!;
        public ProductModel Product { get; private set; } = null!;

        public DetailInvoiceModel(uint detailInvoiceId, uint invoiceId, string productBarcode, uint detailInvoiceQuantity, decimal detailInvoicePrice, bool detailInvoiceStatus)
        {
            DetailInvoiceId = detailInvoiceId;
            InvoiceId = invoiceId;
            ProductBarcode = productBarcode;
            DetailInvoiceQuantity = detailInvoiceQuantity;
            DetailInvoicePrice = detailInvoicePrice;
            DetailInvoiceStatus = detailInvoiceStatus;
        }

        // We use the single primary key for this entity
        public uint GetIdEntity() => DetailInvoiceId;
    }
}

