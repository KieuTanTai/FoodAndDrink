// File: InventoryMovement.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryMovementModel : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryMovementId { get; set; }

        // Corresponds to 'source_location_id' (INT UNSIGNED)
        public uint SourceLocationId { get; set; }

        // Corresponds to 'destination_location_id' (INT UNSIGNED)
        public uint DestinationLocationId { get; set; }

        // Corresponds to 'inventory_movement_quantity' (INT UNSIGNED)
        public uint InventoryMovementQuantity { get; set; }

        // Corresponds to 'inventory_movement_date' (DATETIME)
        public DateTime InventoryMovementDate { get; set; }

        // Corresponds to 'inventory_movement_reason' (TEXT)
        public EInventoryMovementReason InventoryMovementReason { get; set; }

        // Navigation properties
        public LocationModel SourceLocation { get; set; } = null!;
        public LocationModel DestinationLocation { get; set; } = null!;

        public InventoryMovementModel(uint inventoryMovementId, uint sourceLocationId, uint destinationLocationId, uint inventoryMovementQuantity, DateTime inventoryMovementDate, EInventoryMovementReason inventoryMovementReason)
        {
            InventoryMovementId = inventoryMovementId;
            SourceLocationId = sourceLocationId;
            DestinationLocationId = destinationLocationId;
            InventoryMovementQuantity = inventoryMovementQuantity;
            InventoryMovementDate = inventoryMovementDate;
            InventoryMovementReason = inventoryMovementReason;
        }

        public InventoryMovementModel() { }

        public uint GetIdEntity() => InventoryMovementId;
    }
}

