// File: Inventory.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryModel : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryId { get; set; }

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId { get; set; }

        // Corresponds to 'inventory_status' (TINYINT)
        public byte InventoryStatus { get; set; }

        // Corressponds to 'inventory_last_updated_date' (DATETIME)
        public DateTime InventoryLastUpdatedDate { get; set; }

        // Navigation properties
        public LocationModel Location { get; set; } = null!;

        public InventoryModel(uint inventoryId, uint locationId, byte inventoryStatus, DateTime inventoryLastUpdatedDate)
        {
            InventoryId = inventoryId;
            LocationId = locationId;
            InventoryStatus = inventoryStatus;
            InventoryLastUpdatedDate = inventoryLastUpdatedDate;
        }

        public InventoryModel() { }
        public uint GetIdEntity() => InventoryId;
    }
}

