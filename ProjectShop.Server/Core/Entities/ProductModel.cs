// File: Product.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductModel : IGetIdEntity<string>
    {
        // Corresponds to 'product_barcode' (VARCHAR(20)) - Primary Key
        public string ProductBarcode { get; private set; }

        // Corresponds to 'supplier_id' (INT UNSIGNED)
        public uint SupplierId { get; private set; }

        // Corresponds to 'product_name' (NVARCHAR(255))
        public string ProductName { get; private set; }

        // Corresponds to 'product_net_weight' (DECIMAL(10, 2))
        public decimal ProductNetWeight { get; private set; }

        // Corresponds to 'product_weight_range' (VARCHAR(50))
        public string ProductWeightRange { get; private set; }

        // Corresponds to 'product_unit' (ENUM)
        public EProductUnit ProductUnit { get; private set; }

        // Corresponds to 'product_base_price' (DECIMAL(10, 2))
        public decimal ProductBasePrice { get; private set; }

        // Corresponds to 'product_rating_age' (VARCHAR(3))
        public string ProductRatingAge { get; private set; }

        // Corresponds to 'product_status' (TINYINT(1))
        public bool ProductStatus { get; private set; }
        // Corresponds to 'product_created_at' (DATETIME) - Automatically set to current time
        public DateTime ProductAddedDate { get; private set; } = DateTime.UtcNow;

        // Corresponds to 'product_last_updated_date' (DATETIME) - Automatically set to current time
        public DateTime ProductLastUpdatedDate { get; private set; } = DateTime.UtcNow;

        // Navigation properties
        public SupplierModel Supplier { get; private set; } = null!;
        public ICollection<DetailCartModel> DetailCarts { get; private set; } = new List<DetailCartModel>();
        public ICollection<ProductLotModel> ProductLots { get; private set; } = new List<ProductLotModel>();
        public ICollection<ProductCategoriesModel> ProductCategories { get; private set; } = new List<ProductCategoriesModel>();
        public ICollection<ProductImageModel> ProductImages { get; private set; } = new List<ProductImageModel>();
        public ICollection<DetailSaleEventModel> DetailSaleEvents { get; private set; } = new List<DetailSaleEventModel>();
        public ICollection<DetailInvoiceModel> DetailInvoices { get; private set; } = new List<DetailInvoiceModel>();

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

        public string GetIdEntity() => ProductBarcode;
    }
}

