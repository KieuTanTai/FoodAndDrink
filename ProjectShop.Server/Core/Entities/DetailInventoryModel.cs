using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInventoryModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _detailInventoryId;
        private uint _inventoryId;
        private string _productBarcode = string.Empty;
        private uint _detailInventoryQuantity;
        private DateTime _detailInventoryAddedDate;
        private DateTime _detailInventoryLastUpdatedDate;

        // Corresponds to 'inventory_detail_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailInventoryId
        {
            get => _detailInventoryId;
            set => _detailInventoryId = value;
        }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'inventory_detail_quantity' (INT UNSIGNED)
        public uint DetailInventoryQuantity
        {
            get => _detailInventoryQuantity;
            set => _detailInventoryQuantity = value;
        }

        // Corresponds to 'inventory_detail_added_date' (DATETIME)
        public DateTime DetailInventoryAddedDate
        {
            get => _detailInventoryAddedDate;
            set => _detailInventoryAddedDate = value;
        }

        // Corresponds to 'inventory_detail_last_updated_date' (DATETIME)
        public DateTime DetailInventoryLastUpdatedDate
        {
            get => _detailInventoryLastUpdatedDate;
            set => _detailInventoryLastUpdatedDate = value;
        }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public InventoryModel Inventory { get; set; } = null!;
        // End of navigation properties

        public DetailInventoryModel(uint detailInventoryId, uint inventoryId, string productBarcode, uint detailInventoryQuantity, DateTime detailInventoryAddedDate, DateTime detailInventoryLastUpdatedDate)
        {
            DetailInventoryId = detailInventoryId;
            InventoryId = inventoryId;
            ProductBarcode = productBarcode;
            DetailInventoryQuantity = detailInventoryQuantity;
            DetailInventoryAddedDate = detailInventoryAddedDate;
            DetailInventoryLastUpdatedDate = detailInventoryLastUpdatedDate;
        }

        public DetailInventoryModel()
        {
        }

        public uint GetIdEntity() => DetailInventoryId;
    }
}
