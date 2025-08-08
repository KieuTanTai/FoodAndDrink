// File: NewsCategory.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class NewsCategory : IGetIdEntity<uint>
    {
        // Corresponds to 'news_category_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint NewsCategoryId { get; private set; }

        // Corresponds to 'news_category_name' (VARCHAR(30))
        public string NewsCategoryName { get; private set; }

        // Corresponds to 'news_category_status' (TINYINT(1))
        public bool NewsCategoryStatus { get; private set; }

        // Navigation property
        public ICollection<News> News { get; private set; } = new List<News>();

        public NewsCategory(uint newsCategoryId, string newsCategoryName, bool newsCategoryStatus)
        {
            NewsCategoryId = newsCategoryId;
            NewsCategoryName = newsCategoryName;
            NewsCategoryStatus = newsCategoryStatus;
        }

        public uint GetIdEntity() => NewsCategoryId;
    }
}

