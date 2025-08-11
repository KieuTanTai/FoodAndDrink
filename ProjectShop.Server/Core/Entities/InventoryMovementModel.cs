// File: InventoryMovement.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryMovementModel : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryMovementId { get; private set; }

        // Corresponds to 'source_location_id' (INT UNSIGNED)
        public uint SourceLocationId { get; private set; }

        // Corresponds to 'destination_location_id' (INT UNSIGNED)
        public uint DestinationLocationId { get; private set; }

        // Corresponds to 'inventory_movement_quantity' (INT UNSIGNED)
        public uint InventoryMovementQuantity { get; private set; }

        // Corresponds to 'inventory_movement_date' (DATETIME)
        public DateTime InventoryMovementDate { get; private set; }

        // Corresponds to 'inventory_movement_reason' (TEXT)
        public EInventoryMovementReason InventoryMovementReason { get; private set; }

        // Navigation properties
        public LocationModel SourceLocation { get; private set; } = null!;
        public LocationModel DestinationLocation { get; private set; } = null!;

        public InventoryMovementModel(uint inventoryMovementId, uint sourceLocationId, uint destinationLocationId, uint inventoryMovementQuantity, DateTime inventoryMovementDate, EInventoryMovementReason inventoryMovementReason)
        {
            InventoryMovementId = inventoryMovementId;
            SourceLocationId = sourceLocationId;
            DestinationLocationId = destinationLocationId;
            InventoryMovementQuantity = inventoryMovementQuantity;
            InventoryMovementDate = inventoryMovementDate;
            InventoryMovementReason = inventoryMovementReason;
        }

        public uint GetIdEntity() => InventoryMovementId;
    }
}

