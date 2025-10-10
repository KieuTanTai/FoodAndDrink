using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductDrink
{
    public string ProductBarcode { get; set; } = null!;

    /// <summary>
    /// Percentage
    /// </summary>
    public decimal AlcoholContent { get; set; }

    public string BeverageType { get; set; } = null!;

    public decimal VolumeMl { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
