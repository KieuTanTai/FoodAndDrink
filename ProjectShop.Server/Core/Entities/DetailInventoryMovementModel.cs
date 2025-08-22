using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInventoryMovementModel : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailInventoryMovementId { get; set; }

        // Corresponds to 'inventory_movement_id' (INT UNSIGNED)
        public uint InventoryMovementId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Corresponds to 'product_lot_id' (INT UNSIGNED)
        public uint ProductLotId { get; set; }

        // Corresponds to 'detail_inventory_movement_quantity' (INT UNSIGNED)
        public uint DetailInventoryMovementQuantity { get; set; }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public InventoryMovementModel InventoryMovement { get; set; } = null!;
        public ProductLotModel ProductLot { get; set; } = null!;
        // End of navigation properties

        public DetailInventoryMovementModel(uint detailInventoryMovementId, uint inventoryMovementId, string productBarcode, uint productLotId, uint detailInventoryMovementQuantity)
        {
            DetailInventoryMovementId = detailInventoryMovementId;
            InventoryMovementId = inventoryMovementId;
            ProductBarcode = productBarcode;
            ProductLotId = productLotId;
            DetailInventoryMovementQuantity = detailInventoryMovementQuantity;
        }

        public DetailInventoryMovementModel() { }

        public uint GetIdEntity() => DetailInventoryMovementId;
    }
}
