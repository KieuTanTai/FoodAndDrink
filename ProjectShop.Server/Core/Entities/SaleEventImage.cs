using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class SaleEventImage
{
    public uint SaleEventImageId { get; set; }

    public uint SaleEventId { get; set; }

    public string SaleEventImageUrl { get; set; } = null!;

    public DateTime SaleEventImageCreatedDate { get; set; }

    public DateTime SaleEventImageLastUpdatedDate { get; set; }

    public virtual SaleEvent SaleEvent { get; set; } = null!;
}
