using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInventoryMovementModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _detailInventoryMovementId;
        private uint _inventoryMovementId;
        private string _productBarcode = string.Empty;
        private uint _productLotId;
        private uint _detailInventoryMovementQuantity;

        // Corresponds to 'detail_inventory_movement_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailInventoryMovementId
        {
            get => _detailInventoryMovementId;
            set => _detailInventoryMovementId = value;
        }

        // Corresponds to 'inventory_movement_id' (INT UNSIGNED)
        public uint InventoryMovementId
        {
            get => _inventoryMovementId;
            set => _inventoryMovementId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'product_lot_id' (INT UNSIGNED)
        public uint ProductLotId
        {
            get => _productLotId;
            set => _productLotId = value;
        }

        // Corresponds to 'detail_inventory_movement_quantity' (INT UNSIGNED)
        public uint DetailInventoryMovementQuantity
        {
            get => _detailInventoryMovementQuantity;
            set => _detailInventoryMovementQuantity = value;
        }

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
