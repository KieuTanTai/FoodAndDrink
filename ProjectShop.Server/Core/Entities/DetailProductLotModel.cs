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
        // Backing fields
        private uint _productLotId;
        private string _productBarcode = string.Empty;
        private DateTime _productLotMfgDate;
        private DateTime _productLotExpDate;
        private int _productLotInitialQuantity;

        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId
        {
            get => _productLotId;
            set => _productLotId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'product_lot_mfg_date' (DATETIME)
        public DateTime ProductLotMfgDate
        {
            get => _productLotMfgDate;
            set => _productLotMfgDate = value;
        }

        // Corresponds to 'product_lot_exp_date' (DATETIME)
        public DateTime ProductLotExpDate
        {
            get => _productLotExpDate;
            set => _productLotExpDate = value;
        }

        // Corresponds to 'product_lot_initial_quantity' (INT)
        public int ProductLotInitialQuantity
        {
            get => _productLotInitialQuantity;
            set => _productLotInitialQuantity = value;
        }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public ProductLotModel ProductLot { get; set; } = null!;
        // End of navigation properties

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
