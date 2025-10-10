using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class InventoryMovement
{
    public uint InventoryMovementId { get; set; }

    public uint SourceLocationId { get; set; }

    public uint DestinationLocationId { get; set; }

    public uint InventoryMovementQuantity { get; set; }

    public DateTime InventoryMovementDate { get; set; }

    public string InventoryMovementReason { get; set; } = null!;

    public virtual Location DestinationLocation { get; set; } = null!;

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; set; } = new List<DetailInventoryMovement>();

    public virtual Location SourceLocation { get; set; } = null!;
}
