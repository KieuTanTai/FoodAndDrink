using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class SaleEvent
{
    public uint SaleEventId { get; init; }

    public DateTime SaleEventStartDate { get; set; }

    public DateTime SaleEventEndDate { get; set; }

    public string SaleEventName { get; set; } = null!;

    public bool? SaleEventStatus { get; set; }

    public string SaleEventDescription { get; set; } = null!;

    public string SaleEventDiscountCode { get; set; } = null!;

    public virtual ICollection<DetailSaleEvent> DetailSaleEvents { get; init; } = [];

    public virtual ICollection<SaleEventImage> SaleEventImages { get; init; } = [];

    public virtual ICollection<Invoice> Invoices { get; init; } = [];
}
