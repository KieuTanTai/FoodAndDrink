// File: Category.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Category : IGetIdEntity<uint>
    {
        // Corresponds to 'category_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CategoryId { get; private set; }

        // Corresponds to 'category_name' (VARCHAR(255))
        public string CategoryName { get; private set; }

        // Corresponds to 'category_status' (TINYINT(1))
        public bool CategoryStatus { get; private set; }

        // Navigation property
        public ICollection<ProductCategories> ProductCategories { get; private set; } = new List<ProductCategories>();

        public Category(uint categoryId, string categoryName, bool categoryStatus)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryStatus = categoryStatus;
        }

        public uint GetIdEntity() => CategoryId;
    }
}

