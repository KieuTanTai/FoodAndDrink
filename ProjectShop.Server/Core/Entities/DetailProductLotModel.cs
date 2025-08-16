using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public readonly struct DetailProductLotKey
    {
        public uint ProductLotId { get; }
        public string ProductBarcode { get; }
        public DetailProductLotKey(uint productLotId, string productBarcode)
        {
            ProductLotId = productLotId;
            ProductBarcode = productBarcode;
        }
    }

    public class DetailProductLotModel : IGetIdEntity<DetailProductLotKey>
    {
        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Corresponds to 'product_lot_mfg_date' (DATETIME)
        public DateTime ProductLotMfgDate { get; set; }

        // Corresponds to 'product_lot_exp_date' (DATETIME)
        public DateTime ProductLotExpDate { get; set; }

        // Corresponds to 'product_lot_initial_quantity' (INT)
        public int ProductLotInitialQuantity { get; set; }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public ProductLotModel ProductLot { get; set; } = null!;

        public DetailProductLotModel(uint productLotId, string productBarcode, DateTime productLotMfgDate, DateTime productLotExpDate, int productLotInitialQuantity)
        {
            ProductLotId = productLotId;
            ProductBarcode = productBarcode;
            ProductLotMfgDate = productLotMfgDate;
            ProductLotExpDate = productLotExpDate;
            ProductLotInitialQuantity = productLotInitialQuantity;
        }

        public DetailProductLotModel() { }

        public DetailProductLotKey GetIdEntity() => new DetailProductLotKey(ProductLotId, ProductBarcode);
    }
}
