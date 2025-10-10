using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductFruit
{
    public string ProductBarcode { get; set; } = null!;

    public string Season { get; set; } = null!;

    public string SweetnessLevel { get; set; } = null!;

    public string FruitType { get; set; } = null!;

    public bool IsOrganic { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
