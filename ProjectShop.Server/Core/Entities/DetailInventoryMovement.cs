using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInventoryMovement
{
    public uint DetailInventoryMovementId { get; set; }

    public uint InventoryMovementId { get; set; }

    public string ProductBarcode { get; set; } = null!;

    public uint? ProductLotId { get; set; }

    public uint DetailInventoryMovementQuantity { get; set; }

    public virtual InventoryMovement InventoryMovement { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;

    public virtual ProductLot? ProductLot { get; set; }
}
