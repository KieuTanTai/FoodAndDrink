using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductLot
{
    public uint ProductLotId { get; init; }

    public uint InventoryId { get; private set; }

    public DateTime ProductLotCreatedDate { get; init; }

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; init; } = [];

    public virtual Inventory Inventory { get; set; } = null!;
}
