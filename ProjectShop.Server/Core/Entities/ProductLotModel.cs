// File: ProductLot.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductLotModel : IGetIdEntity<uint>
    {
        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId { get; private set; }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId { get; private set; }

        // Corresponds to 'product_lot_create_date' (DATETIME)
        public DateTime ProductLotCreateDate { get; private set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<InventoryModel> Inventories { get; private set; } = new List<InventoryModel>();

        public ProductLotModel(uint productLotId, uint inventoryId, DateTime productLotCreateDate)
        {
            ProductLotId = productLotId;
            InventoryId = inventoryId;
            ProductLotCreateDate = productLotCreateDate;
        }

        public uint GetIdEntity() => ProductLotId;
    }
}

