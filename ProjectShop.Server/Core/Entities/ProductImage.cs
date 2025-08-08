// File: ProductImage.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductImage : IGetIdEntity<uint>
    {
        // Corresponds to 'product_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductImageId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }
        public Product Product { get; private set; } = null!;

        // Corresponds to 'image_url' (VARCHAR(255))
        public string ImageUrl { get; private set; }

        // Corresponds to 'product_image_priority' (INT)
        public int ProductImagePriority { get; private set; }

        public ProductImage(uint productImageId, string productBarcode, string imageUrl, int productImagePriority)
        {
            ProductImageId = productImageId;
            ProductBarcode = productBarcode;
            ImageUrl = imageUrl;
            ProductImagePriority = productImagePriority;
        }

        public uint GetIdEntity() => ProductImageId;
    }
}

