using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductMeat
{
    public string ProductBarcode { get; init; } = null!;

    public string MeatType { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public bool IsFrozen { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
