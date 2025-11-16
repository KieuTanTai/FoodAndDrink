using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class InventoryMovement
{
    public uint InventoryMovementId { get; init; }

    public uint SourceLocationId { get; private set; }

    public uint DestinationLocationId { get; private set; }

    public uint InventoryMovementQuantity { get; set; }

    public DateTime InventoryMovementDate { get; init; }

    public string InventoryMovementReason { get; set; } = null!;

    public virtual Location DestinationLocation { get; set; } = null!;

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; set; } = [];

    public virtual Location SourceLocation { get; set; } = null!;
}
