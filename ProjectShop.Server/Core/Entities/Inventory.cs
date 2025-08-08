// File: Inventory.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class Inventory : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryId { get; private set; }

        // Corresponds to 'product_lot_id' (INT UNSIGNED)
        public uint ProductLotId { get; private set; }
        public ProductLot ProductLot { get; private set; } = null!;

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId { get; private set; }
        public Location Location { get; private set; } = null!;

        // Corresponds to 'inventory_current_quantity' (INT UNSIGNED)
        public uint InventoryCurrentQuantity { get; private set; }

        public Inventory(uint inventoryId, uint productLotId, uint locationId, uint inventoryCurrentQuantity)
        {
            InventoryId = inventoryId;
            ProductLotId = productLotId;
            LocationId = locationId;
            InventoryCurrentQuantity = inventoryCurrentQuantity;
        }

        public uint GetIdEntity() => InventoryId;
    }
}

