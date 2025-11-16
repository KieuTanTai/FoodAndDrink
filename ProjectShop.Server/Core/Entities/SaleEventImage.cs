using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class SaleEventImage
{
    public uint SaleEventImageId { get; init; }

    public uint SaleEventId { get; private set; }

    public string SaleEventImageUrl { get; set; } = null!;

    public DateTime SaleEventImageCreatedDate { get; init; }

    public DateTime SaleEventImageLastUpdatedDate { get; set; }

    public virtual SaleEvent SaleEvent { get; set; } = null!;
}
