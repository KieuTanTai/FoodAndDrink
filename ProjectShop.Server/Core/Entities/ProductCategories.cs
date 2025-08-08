// File: ProductCategories.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    // A struct to represent the composite key for this table
    public struct ProductCategoriesKey
    {
        public uint CategoryId { get; }
        public string ProductBarcode { get; }

        public ProductCategoriesKey(uint categoryId, string productBarcode)
        {
            CategoryId = categoryId;
            ProductBarcode = productBarcode;
        }
    }

    public class ProductCategories : IGetIdEntity<ProductCategoriesKey>
    {
        // Corresponds to 'category_id' (INT UNSIGNED)
        public uint CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }
        public Product Product { get; private set; } = null!;

        public ProductCategories(uint categoryId, string productBarcode)
        {
            CategoryId = categoryId;
            ProductBarcode = productBarcode;
        }

        public ProductCategoriesKey GetIdEntity() => new ProductCategoriesKey(CategoryId, ProductBarcode);
    }
}

