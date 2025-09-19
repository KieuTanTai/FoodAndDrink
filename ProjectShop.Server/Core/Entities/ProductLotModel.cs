// File: ProductLot.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductLotModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _productLotId;
        private uint _inventoryId;
        private DateTime _productLotCreatedDate = DateTime.UtcNow;

        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId
        {
            get => _productLotId;
            set => _productLotId = value;
        }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        // Corresponds to 'product_lot_create_date' (DATETIME)
        public DateTime ProductLotCreatedDate
        {
            get => _productLotCreatedDate;
            set => _productLotCreatedDate = value;
        }

        // Navigation properties
        public ICollection<InventoryModel> Inventories { get; set; } = new List<InventoryModel>();
        public ICollection<DetailInventoryMovementModel> DetailInventoryMovements { get; set; } = new List<DetailInventoryMovementModel>();
        // End of Navigation properties

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

