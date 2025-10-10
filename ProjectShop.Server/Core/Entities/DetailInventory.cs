using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInventory
{
    public uint DetailInventoryId { get; set; }

    public uint InventoryId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public uint DetailInventoryQuantity { get; set; }

    public DateTime DetailInventoryAddedDate { get; set; }

    public DateTime DetailInventoryLastUpdatedDate { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;
}
