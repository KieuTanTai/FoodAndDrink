using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailInventoryModel : IGetIdEntity<uint>
    {
        // Corresponds to 'inventory_detail_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailInventoryId { get; set; }

        // Corresponds to 'inventory_id' (INT UNSIGNED)
        public uint InventoryId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Corresponds to 'inventory_detail_quantity' (INT UNSIGNED)
        public uint DetailInventoryQuantity { get; set; }

        // Corresponds to 'inventory_detail_added_date' (DATETIME)
        public DateTime DetailInventoryAddedDate { get; set; }

        // Corresponds to 'inventory_detail_last_updated_date' (DATETIME)
        public DateTime DetailInventoryLastUpdatedDate { get; set; }

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
