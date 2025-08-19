// File: InvoiceDiscount.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

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
    public class InvoiceDiscountModel : IGetIdEntity<InvoiceDiscountKey>
    {
        // Corresponds to 'invoice_id' (INT UNSIGNED)
        public uint InvoiceId { get; set; }

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId { get; set; }

        // Navigation properties
        public SaleEventModel SaleEvent { get; set; } = null!;
        public InvoiceModel Invoice { get; set; } = null!;
        // End of navigation properties

        public InvoiceDiscountModel(uint invoiceId, uint saleEventId)
        {
            InvoiceId = invoiceId;
            SaleEventId = saleEventId;
        }

        public InvoiceDiscountModel() { }

        public InvoiceDiscountKey GetIdEntity() => new InvoiceDiscountKey(InvoiceId, SaleEventId);
    }
}

