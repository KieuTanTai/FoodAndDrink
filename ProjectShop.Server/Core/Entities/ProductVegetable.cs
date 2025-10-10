using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductVegetable
{
    public string ProductBarcode { get; set; } = null!;

    public string VegetableType { get; set; } = null!;

    public string StorageMethod { get; set; } = null!;

    public string HarvestSeason { get; set; } = null!;

    public bool IsOrganic { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
