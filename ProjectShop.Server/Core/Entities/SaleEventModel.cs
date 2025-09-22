// File: SaleEvent.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SaleEventModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _saleEventId;
        private DateTime _saleEventStartDate;
        private DateTime _saleEventEndDate;
        private string _saleEventName = string.Empty;
        private bool _saleEventStatus;
        private string _saleEventDescription = string.Empty;
        private string _saleEventDiscountCode = string.Empty;

        // Corresponds to 'sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SaleEventId
        {
            get => _saleEventId;
            set => _saleEventId = value;
        }

        // Corresponds to 'sale_event_start_date' (DATETIME)
        public DateTime SaleEventStartDate
        {
            get => _saleEventStartDate;
            set => _saleEventStartDate = value;
        }

        // Corresponds to 'sale_event_end_date' (DATETIME)
        public DateTime SaleEventEndDate
        {
            get => _saleEventEndDate;
            set => _saleEventEndDate = value;
        }

        // Corresponds to 'sale_event_name' (VARCHAR(40))
        public string SaleEventName
        {
            get => _saleEventName;
            set => _saleEventName = value ?? string.Empty;
        }

        // Corresponds to 'sale_event_status' (TINYINT(1))
        public bool SaleEventStatus
        {
            get => _saleEventStatus;
            set => _saleEventStatus = value;
        }

        // Corresponds to 'sale_event_description' (TEXT)
        public string SaleEventDescription
        {
            get => _saleEventDescription;
            set => _saleEventDescription = value ?? string.Empty;
        }

        // Corresponds to 'sale_event_discount_code' (VARCHAR(20))
        public string SaleEventDiscountCode
        {
            get => _saleEventDiscountCode;
            set => _saleEventDiscountCode = value ?? string.Empty;
        }

        // Navigation properties
        public ICollection<DetailSaleEventModel> DetailSaleEvents { get; set; } = [];
        public ICollection<InvoiceDiscountModel> InvoiceDiscounts { get; set; } = [];
        // End of navigation properties

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

