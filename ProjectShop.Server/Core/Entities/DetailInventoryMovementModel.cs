using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInventoryMovementModel : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailInventoryMovementId { get; private set; }

        // Corresponds to 'inventory_movement_id' (INT UNSIGNED)
        public uint InventoryMovementId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }

        // Corresponds to 'detail_inventory_movement_quantity' (INT UNSIGNED)
        public uint DetailInventoryMovementQuantity { get; private set; }

        // Navigation property 
        public ProductModel Product { get; private set; } = null!;
        public InventoryMovementModel InventoryMovement { get; private set; } = null!;

        public DetailInventoryMovementModel(uint detailInventoryMovementId, uint inventoryMovementId, string productBarcode, uint detailInventoryMovementQuantity)
        {
            DetailInventoryMovementId = detailInventoryMovementId;
            InventoryMovementId = inventoryMovementId;
            ProductBarcode = productBarcode;
            DetailInventoryMovementQuantity = detailInventoryMovementQuantity;
        }

        public uint GetIdEntity() => DetailInventoryMovementId;
    }
}
