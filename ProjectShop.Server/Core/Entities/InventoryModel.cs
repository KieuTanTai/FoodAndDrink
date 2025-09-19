// File: Inventory.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _inventoryId;
        private uint _locationId;
        private byte _inventoryStatus;
        private DateTime _inventoryLastUpdatedDate;

        // Corresponds to 'inventory_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId
        {
            get => _locationId;
            set => _locationId = value;
        }

        // Corresponds to 'inventory_status' (TINYINT)
        public byte InventoryStatus
        {
            get => _inventoryStatus;
            set => _inventoryStatus = value;
        }

        // Corressponds to 'inventory_last_updated_date' (DATETIME)
        public DateTime InventoryLastUpdatedDate
        {
            get => _inventoryLastUpdatedDate;
            set => _inventoryLastUpdatedDate = value;
        }

        // Navigation properties
        public LocationModel Location { get; set; } = null!;
        // End of navigation properties

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

