// File: Category.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CategoryModel : IGetIdEntity<uint>
    {
        // Corresponds to 'category_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CategoryId { get; set; }

        // Corresponds to 'category_name' (VARCHAR(255))
        public string CategoryName { get; set; } = string.Empty;

        // Corresponds to 'category_status' (TINYINT(1))
        public bool CategoryStatus { get; set; }

        // Navigation properties
        public ICollection<ProductCategoriesModel> ProductCategories { get; set; } = new List<ProductCategoriesModel>();
        // End of navigation properties

        public CategoryModel(uint categoryId, string categoryName, bool categoryStatus)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryStatus = categoryStatus;
        }

        public CategoryModel()
        {
        }
        public uint GetIdEntity() => CategoryId;
    }
}

