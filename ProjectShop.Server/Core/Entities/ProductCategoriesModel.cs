// File: ProductCategories.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

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

    public class ProductCategoriesModel : IGetIdEntity<ProductCategoriesKey>
    {
        // Corresponds to 'id' (INT UNSIGNED AUTO_INCREMENT)
        public uint Id { get; set; }

        // Corresponds to 'category_id' (INT UNSIGNED)
        public uint CategoryId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Navigation properties
        public CategoryModel Category { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
        // End of navigation properties

        public ProductCategoriesModel(uint id, uint categoryId, string productBarcode)
        {
            Id = id;
            CategoryId = categoryId;
            ProductBarcode = productBarcode;
        }

        public ProductCategoriesModel() { }

        public ProductCategoriesKey GetIdEntity() => new ProductCategoriesKey(CategoryId, ProductBarcode);
    }
}

