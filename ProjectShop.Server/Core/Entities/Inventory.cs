using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Inventory
{
    public uint InventoryId { get; set; }

    public uint LocationId { get; set; }

    public bool? InventoryStatus { get; set; }

    public DateTime InventoryLastUpdatedDate { get; set; }

    public virtual ICollection<DetailInventory> DetailInventories { get; set; } = new List<DetailInventory>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<ProductLot> ProductLots { get; set; } = new List<ProductLot>();
}
