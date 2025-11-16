using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class DetailInventoryMovement
{
    public uint DetailInventoryMovementId { get; init; }

    public uint InventoryMovementId { get; private set; }

    public string ProductBarcode { get; private set; } = null!;

    public uint? ProductLotId { get; private set; }

    public uint DetailInventoryMovementQuantity { get; set; }

    public virtual InventoryMovement InventoryMovement { get; set; } = null!;

    public virtual Product ProductBarcodeNavigation { get; set; } = null!;

    public virtual ProductLot? ProductLot { get; set; }
}
