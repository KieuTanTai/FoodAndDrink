// File: Product.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductModel : IGetIdEntity<string>
    {
        // Backing fields
        private string _productBarcode = string.Empty;
        private uint _supplierId;
        private string _productName = string.Empty;
        private decimal _productNetWeight;
        private string _productWeightRange = string.Empty;
        private EProductUnit _productUnit;
        private decimal _productBasePrice;
        private string _productRatingAge = string.Empty;
        private bool _productStatus;
        private DateTime _productAddedDate = DateTime.UtcNow;
        private DateTime _productLastUpdatedDate = DateTime.UtcNow;

        // Corresponds to 'product_barcode' (VARCHAR(20)) - Primary Key
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'supplier_id' (INT UNSIGNED)
        public uint SupplierId
        {
            get => _supplierId;
            set => _supplierId = value;
        }

        // Corresponds to 'product_name' (NVARCHAR(255))
        public string ProductName
        {
            get => _productName;
            set => _productName = value ?? string.Empty;
        }

        // Corresponds to 'product_net_weight' (DECIMAL(10, 2))
        public decimal ProductNetWeight
        {
            get => _productNetWeight;
            set => _productNetWeight = value;
        }

        // Corresponds to 'product_weight_range' (VARCHAR(50))
        public string ProductWeightRange
        {
            get => _productWeightRange;
            set => _productWeightRange = value ?? string.Empty;
        }

        // Corresponds to 'product_unit' (ENUM)
        public EProductUnit ProductUnit
        {
            get => _productUnit;
            set => _productUnit = value;
        }

        // Corresponds to 'product_base_price' (DECIMAL(10, 2))
        public decimal ProductBasePrice
        {
            get => _productBasePrice;
            set => _productBasePrice = value;
        }

        // Corresponds to 'product_rating_age' (VARCHAR(3))
        public string ProductRatingAge
        {
            get => _productRatingAge;
            set => _productRatingAge = value ?? string.Empty;
        }

        // Corresponds to 'product_status' (TINYINT(1))
        public bool ProductStatus
        {
            get => _productStatus;
            set => _productStatus = value;
        }

        // Corresponds to 'product_created_at' (DATETIME) - Automatically set to current time
        public DateTime ProductAddedDate
        {
            get => _productAddedDate;
            set => _productAddedDate = value;
        }

        // Corresponds to 'product_last_updated_date' (DATETIME) - Automatically set to current time
        public DateTime ProductLastUpdatedDate
        {
            get => _productLastUpdatedDate;
            set => _productLastUpdatedDate = value;
        }

        // Navigation properties
        public SupplierModel Supplier { get; set; } = null!;
        public ICollection<DetailCartModel> DetailCarts { get; set; } = new List<DetailCartModel>();
        public ICollection<DetailProductLotModel> DetailProductLot { get; set; } = new List<DetailProductLotModel>();
        public ICollection<ProductCategoriesModel> ProductCategories { get; set; } = new List<ProductCategoriesModel>();
        public ICollection<ProductImageModel> ProductImages { get; set; } = new List<ProductImageModel>();
        public ICollection<DetailSaleEventModel> DetailSaleEvents { get; set; } = new List<DetailSaleEventModel>();
        public ICollection<DetailInvoiceModel> DetailInvoices { get; set; } = new List<DetailInvoiceModel>();

        // NEW
        public ICollection<DetailInventoryMovementModel> DetailInventoryMovements { get; set; } = new List<DetailInventoryMovementModel>();
        public ICollection<DetailInventoryModel> DetailInventories { get; set; } = new List<DetailInventoryModel>();
        public ICollection<DisposeProductModel> DisposeProducts { get; set; } = new List<DisposeProductModel>();
        // End of navigation properties

        public ProductModel(string productBarcode, uint supplierId, string productName, decimal productNetWeight, string productWeightRange, EProductUnit productUnit, decimal productBasePrice,
                string productRatingAge, bool productStatus, DateTime productAddedDate, DateTime productLastUpdatedDate)
        {
            ProductBarcode = productBarcode;
            SupplierId = supplierId;
            ProductName = productName;
            ProductNetWeight = productNetWeight;
            ProductWeightRange = productWeightRange;
            ProductUnit = productUnit;
            ProductBasePrice = productBasePrice;
            ProductRatingAge = productRatingAge;
            ProductStatus = productStatus;
            ProductAddedDate = productAddedDate;
            ProductLastUpdatedDate = productLastUpdatedDate;
        }

        public ProductModel()
        {
        }

        public string GetIdEntity() => ProductBarcode;
    }
}

