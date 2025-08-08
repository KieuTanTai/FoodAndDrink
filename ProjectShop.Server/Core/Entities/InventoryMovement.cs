// File: InventoryMovement.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryMovement : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryMovementId { get; private set; }

        // Corresponds to 'source_location_id' (INT UNSIGNED)
        public uint SourceLocationId { get; private set; }
        public Location SourceLocation { get; private set; } = null!;

        // Corresponds to 'destination_location_id' (INT UNSIGNED)
        public uint DestinationLocationId { get; private set; }
        public Location DestinationLocation { get; private set; } = null!;

        // Corresponds to 'inventory_movement_quantity' (INT UNSIGNED)
        public uint InventoryMovementQuantity { get; private set; }

        // Corresponds to 'inventory_movement_date' (DATETIME)
        public DateTime InventoryMovementDate { get; private set; }

        // Corresponds to 'inventory_movement_reason' (TEXT)
        public string InventoryMovementReason { get; private set; }

        public InventoryMovement(uint inventoryMovementId, uint sourceLocationId, uint destinationLocationId, uint inventoryMovementQuantity, DateTime inventoryMovementDate, string inventoryMovementReason)
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

