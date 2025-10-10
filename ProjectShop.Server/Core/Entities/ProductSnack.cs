using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductSnack
{
    public string ProductBarcode { get; set; } = null!;

    public string Flavor { get; set; } = null!;

    public string SnackType { get; set; } = null!;

    public bool IsGlutenFree { get; set; }

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
