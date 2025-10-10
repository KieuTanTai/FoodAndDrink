using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class ProductLot
{
    public uint ProductLotId { get; set; }

    public uint InventoryId { get; set; }

    public DateTime ProductLotCreatedDate { get; set; }

    public virtual ICollection<DetailInventoryMovement> DetailInventoryMovements { get; set; } = new List<DetailInventoryMovement>();

    public virtual Inventory Inventory { get; set; } = null!;
}
