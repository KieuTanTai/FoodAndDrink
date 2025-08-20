// File: ProductImage.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductImageModel : IGetIdEntity<uint>
    {
        // Corresponds to 'product_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductImageId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Corresponds to 'image_url' (VARCHAR(255))
        public string ProductImageUrl { get; set; } = string.Empty;

        // Corresponds to 'product_image_priority' (INT)
        public int ProductImagePriority { get; set; }

        // Corresponds to 'product_image_create_date' (DATETIME)
        public DateTime ProductImageCreatedDate { get; set; } = DateTime.UtcNow;

        // Corresponds to `ProductImageLastUpdatedDate` (DATETIME)
        public DateTime ProductImageLastUpdatedDate { get; set; } = DateTime.UtcNow;

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

