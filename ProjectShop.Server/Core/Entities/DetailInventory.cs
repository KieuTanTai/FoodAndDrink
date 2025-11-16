using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInventory
{
    public uint DetailInventoryId { get; init; }

    public uint InventoryId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public uint DetailInventoryQuantity { get; set; }

    public DateTime DetailInventoryAddedDate { get; init; }

    public DateTime DetailInventoryLastUpdatedDate { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
