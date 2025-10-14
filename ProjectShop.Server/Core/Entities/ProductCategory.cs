using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductCategory
{
    public uint Id { get; init; }

    public uint CategoryId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
