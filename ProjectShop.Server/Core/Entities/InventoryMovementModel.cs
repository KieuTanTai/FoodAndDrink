// File: InventoryMovement.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class InventoryMovementModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _inventoryMovementId;
        private uint _sourceLocationId;
        private uint _destinationLocationId;
        private uint _inventoryMovementQuantity;
        private DateTime _inventoryMovementDate;
        private EInventoryMovementReason _inventoryMovementReason;

        // Corresponds to 'inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint InventoryMovementId
        {
            get => _inventoryMovementId;
            set => _inventoryMovementId = value;
        }

        // Corresponds to 'source_location_id' (INT UNSIGNED)
        public uint SourceLocationId
        {
            get => _sourceLocationId;
            set => _sourceLocationId = value;
        }

        // Corresponds to 'destination_location_id' (INT UNSIGNED)
        public uint DestinationLocationId
        {
            get => _destinationLocationId;
            set => _destinationLocationId = value;
        }

        // Corresponds to 'inventory_movement_quantity' (INT UNSIGNED)
        public uint InventoryMovementQuantity
        {
            get => _inventoryMovementQuantity;
            set => _inventoryMovementQuantity = value;
        }

        // Corresponds to 'inventory_movement_date' (DATETIME)
        public DateTime InventoryMovementDate
        {
            get => _inventoryMovementDate;
            set => _inventoryMovementDate = value;
        }

        // Corresponds to 'inventory_movement_reason' (TEXT)
        public EInventoryMovementReason InventoryMovementReason
        {
            get => _inventoryMovementReason;
            set => _inventoryMovementReason = value;
        }

        // Navigation properties
        public LocationModel SourceLocation { get; set; } = null!;
        public LocationModel DestinationLocation { get; set; } = null!;
        // End of navigation properties

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

