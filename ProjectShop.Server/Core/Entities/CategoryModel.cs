// File: Category.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CategoryModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _categoryId;
        private string _categoryName = string.Empty;
        private bool _categoryStatus;

        // Corresponds to 'category_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CategoryId
        {
            get => _categoryId;
            set => _categoryId = value;
        }

        // Corresponds to 'category_name' (VARCHAR(255))
        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = value ?? string.Empty;
        }

        // Corresponds to 'category_status' (TINYINT(1))
        public bool CategoryStatus
        {
            get => _categoryStatus;
            set => _categoryStatus = value;
        }

        // Navigation properties
        public ICollection<ProductCategoriesModel> ProductCategories { get; set; } = [];
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

