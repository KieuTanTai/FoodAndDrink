// File: DetailInvoice.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInvoiceModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _detailInvoiceId;
        private uint _invoiceId;
        private string _productBarcode = string.Empty;
        private uint _detailInvoiceQuantity;
        private decimal _detailInvoicePrice;
        private bool _detailInvoiceStatus;

        public uint DetailInvoiceId
        {
            get => _detailInvoiceId;
            set => _detailInvoiceId = value;
        }

        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId
        {
            get => _invoiceId;
            set => _invoiceId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'detail_invoice_quantity' (INT UNSIGNED)
        public uint DetailInvoiceQuantity
        {
            get => _detailInvoiceQuantity;
            set => _detailInvoiceQuantity = value;
        }

        // Corresponds to 'detail_invoice_price' (DECIMAL(10, 2))
        public decimal DetailInvoicePrice
        {
            get => _detailInvoicePrice;
            set => _detailInvoicePrice = value;
        }

        // Corresponds to 'detail_invoice_status' (TINYINT(1))
        public bool DetailInvoiceStatus
        {
            get => _detailInvoiceStatus;
            set => _detailInvoiceStatus = value;
        }

        // Navigation properties
        public InvoiceModel Invoice { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
        // End of navigation properties

        public DetailInvoiceModel(uint detailInvoiceId, uint invoiceId, string productBarcode, uint detailInvoiceQuantity, decimal detailInvoicePrice, bool detailInvoiceStatus)
        {
            DetailInvoiceId = detailInvoiceId;
            InvoiceId = invoiceId;
            ProductBarcode = productBarcode;
            DetailInvoiceQuantity = detailInvoiceQuantity;
            DetailInvoicePrice = detailInvoicePrice;
            DetailInvoiceStatus = detailInvoiceStatus;
        }

        public DetailInvoiceModel() { }

        // We use the single primary key for this entity
        public uint GetIdEntity() => DetailInvoiceId;
    }
}

