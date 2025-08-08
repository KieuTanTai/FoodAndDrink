// File: SaleEvent.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class SaleEvent : IGetIdEntity<uint>
    {
        // Corresponds to 'sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SaleEventId { get; private set; }

        // Corresponds to 'sale_event_start_date' (DATETIME)
        public DateTime SaleEventStartDate { get; private set; }

        // Corresponds to 'sale_event_end_date' (DATETIME)
        public DateTime SaleEventEndDate { get; private set; }

        // Corresponds to 'sale_event_name' (VARCHAR(40))
        public string SaleEventName { get; private set; }

        // Corresponds to 'sale_event_status' (TINYINT(1))
        public bool SaleEventStatus { get; private set; }

        // Corresponds to 'sale_event_description' (TEXT)
        public string SaleEventDescription { get; private set; }

        // Corresponds to 'sale_event_discount_code' (VARCHAR(20))
        public string SaleEventDiscountCode { get; private set; }

        // Navigation properties
        public ICollection<DetailSaleEvent> DetailSaleEvents { get; private set; } = new List<DetailSaleEvent>();
        public ICollection<InvoiceDiscount> InvoiceDiscounts { get; private set; } = new List<InvoiceDiscount>();

        public SaleEvent(uint saleEventId, DateTime saleEventStartDate, DateTime saleEventEndDate, string saleEventName, bool saleEventStatus, string saleEventDescription, string saleEventDiscountCode)
        {
            SaleEventId = saleEventId;
            SaleEventStartDate = saleEventStartDate;
            SaleEventEndDate = saleEventEndDate;
            SaleEventName = saleEventName;
            SaleEventStatus = saleEventStatus;
            SaleEventDescription = saleEventDescription;
            SaleEventDiscountCode = saleEventDiscountCode;
        }

        public uint GetIdEntity() => SaleEventId;
    }
}

