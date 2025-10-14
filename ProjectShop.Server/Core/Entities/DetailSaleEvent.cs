using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailSaleEvent
{
    public uint DetailSaleEventId { get; init; }

    public uint SaleEventId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountPercent { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal MaxDiscountPrice { get; set; }

    public decimal MinPriceToUse { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;

    public virtual SaleEvent SaleEvent { get; set; } = null!;
}
