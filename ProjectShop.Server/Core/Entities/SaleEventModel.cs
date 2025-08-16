// File: SaleEvent.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SaleEventModel : IGetIdEntity<uint>
    {
        // Corresponds to 'sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SaleEventId { get; set; }

        // Corresponds to 'sale_event_start_date' (DATETIME)
        public DateTime SaleEventStartDate { get; set; }

        // Corresponds to 'sale_event_end_date' (DATETIME)
        public DateTime SaleEventEndDate { get; set; }

        // Corresponds to 'sale_event_name' (VARCHAR(40))
        public string SaleEventName { get; set; } = string.Empty;

        // Corresponds to 'sale_event_status' (TINYINT(1))
        public bool SaleEventStatus { get; set; }

        // Corresponds to 'sale_event_description' (TEXT)
        public string SaleEventDescription { get; set; } = string.Empty;

        // Corresponds to 'sale_event_discount_code' (VARCHAR(20))
        public string SaleEventDiscountCode { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<DetailSaleEventModel> DetailSaleEvents { get; set; } = new List<DetailSaleEventModel>();
        public ICollection<InvoiceDiscountModel> InvoiceDiscounts { get; set; } = new List<InvoiceDiscountModel>();

        public SaleEventModel(uint saleEventId, DateTime saleEventStartDate, DateTime saleEventEndDate, string saleEventName, bool saleEventStatus, string saleEventDescription, string saleEventDiscountCode)
        {
            SaleEventId = saleEventId;
            SaleEventStartDate = saleEventStartDate;
            SaleEventEndDate = saleEventEndDate;
            SaleEventName = saleEventName;
            SaleEventStatus = saleEventStatus;
            SaleEventDescription = saleEventDescription;
            SaleEventDiscountCode = saleEventDiscountCode;
        }

        public SaleEventModel()
        {
        }

        public uint GetIdEntity() => SaleEventId;
    }
}

