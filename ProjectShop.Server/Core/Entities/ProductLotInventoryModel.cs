using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public struct ProductLotInventoryKey
    {
        public uint ProductLotId { get; set; }
        public uint InventoryId { get; set; }

        public ProductLotInventoryKey(uint productLotId, uint inventoryId)
        {
            ProductLotId = productLotId;
            InventoryId = inventoryId;
        }
    }

    public class ProductLotInventoryModel : IGetIdEntity<ProductLotInventoryKey>
    {
        // Corresponds to 'product_lot_id' (UINT)
        public uint ProductLotId { get; private set; }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId { get; private set; }

        // Corresponds to 'product_lot_inventory_quantity' (UINT)
        public uint ProductLotInventoryQuantity { get; private set; }

        // Corresponds to 'product_lot_inventory_added_date' (DATETIME)
        public DateTime ProductLotInventoryAddedDate { get; private set; }

        // Navigation properties
        public InventoryModel Inventory { get; private set; } = null!;
        public ICollection<ProductLotModel> ProductLots { get; private set; } = new List<ProductLotModel>();

        public ProductLotInventoryModel(uint inventoryId, uint productLotId, uint productLotInventoryQuantity, DateTime productLotInventoryAddedDate)
        {
            InventoryId = inventoryId;
            ProductLotId = productLotId;
            ProductLotInventoryQuantity = productLotInventoryQuantity;
            ProductLotInventoryAddedDate = productLotInventoryAddedDate;
        }

        public ProductLotInventoryKey GetIdEntity() => new ProductLotInventoryKey(InventoryId, ProductLotId);
    }
}
