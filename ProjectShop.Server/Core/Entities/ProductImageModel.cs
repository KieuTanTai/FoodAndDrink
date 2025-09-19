// File: ProductImage.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductImageModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _productImageId;
        private string _productBarcode = string.Empty;
        private string _productImageUrl = string.Empty;
        private int _productImagePriority;
        private DateTime _productImageCreatedDate = DateTime.UtcNow;
        private DateTime _productImageLastUpdatedDate = DateTime.UtcNow;

        // Corresponds to 'product_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductImageId
        {
            get => _productImageId;
            set => _productImageId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'image_url' (VARCHAR(255))
        public string ProductImageUrl
        {
            get => _productImageUrl;
            set => _productImageUrl = value ?? string.Empty;
        }

        // Corresponds to 'product_image_priority' (INT)
        public int ProductImagePriority
        {
            get => _productImagePriority;
            set => _productImagePriority = value;
        }

        // Corresponds to 'product_image_create_date' (DATETIME)
        public DateTime ProductImageCreatedDate
        {
            get => _productImageCreatedDate;
            set => _productImageCreatedDate = value;
        }

        // Corresponds to `ProductImageLastUpdatedDate` (DATETIME)
        public DateTime ProductImageLastUpdatedDate
        {
            get => _productImageLastUpdatedDate;
            set => _productImageLastUpdatedDate = value;
        }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        // End of navigation properties

        public ProductImageModel(uint productImageId, string productBarcode, string productImageUrl, int productImagePriority, DateTime produtImageCreatedDate, DateTime productImageLastUpdatedDate)
        {
            ProductImageId = productImageId;
            ProductBarcode = productBarcode;
            ProductImageUrl = productImageUrl;
            ProductImagePriority = productImagePriority;
            ProductImageCreatedDate = produtImageCreatedDate;
            ProductImageLastUpdatedDate = productImageLastUpdatedDate;
        }

        public ProductImageModel()
        {
        }

        public uint GetIdEntity() => ProductImageId;
    }
}

