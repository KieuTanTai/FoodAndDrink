// File: ProductLot.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductLotModel : IGetIdEntity<uint>
    {
        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId { get; set; }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId { get; set; }

        // Corresponds to 'product_lot_create_date' (DATETIME)
        public DateTime ProductLotCreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<InventoryModel> Inventories { get; set; } = new List<InventoryModel>();

        public ProductLotModel(uint productLotId, uint inventoryId, DateTime productLotCreatedDate)
        {
            ProductLotId = productLotId;
            InventoryId = inventoryId;
            ProductLotCreatedDate = productLotCreatedDate;
        }

        public ProductLotModel()
        {
        }

        public uint GetIdEntity() => ProductLotId;
    }
}

