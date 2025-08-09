// File: News.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class NewsModel : IGetIdEntity<uint>
    {
        // Corresponds to 'news_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint NewsId { get; private set; }

        // Corresponds to 'employee_id' (INT UNSIGNED)
        public uint EmployeeId { get; private set; }

        // Corresponds to 'news_category_id' (INT UNSIGNED)
        public uint NewsCategoryId { get; private set; }

        // Corresponds to 'news_related_product_barcode' (VARCHAR(20))
        public string NewsRelatedProductBarcode { get; private set; }

        // Corresponds to 'news_title' (VARCHAR(50))
        public string NewsTitle { get; private set; }

        // Corresponds to 'news_published_date' (DATETIME)
        public DateTime NewsPublishedDate { get; private set; }

        // Corresponds to 'news_content' (TEXT)
        public string NewsContent { get; private set; }

        // Corresponds to 'news_status' (TINYINT(1))
        public bool NewsStatus { get; private set; }

        // Navigation property
        public EmployeeModel Employee { get; private set; } = null!;
        public NewsCategory NewsCategory { get; private set; } = null!;
        public ProductModel NewsRelatedProduct { get; private set; } = null!;

        public ICollection<NewsImage> NewsImages { get; private set; } = new List<NewsImage>();

        public NewsModel(uint newsId, uint employeeId, uint newsCategoryId, string newsRelatedProductBarcode, string newsTitle, DateTime newsPublishedDate, string newsContent, bool newsStatus)
        {
            NewsId = newsId;
            EmployeeId = employeeId;
            NewsCategoryId = newsCategoryId;
            NewsRelatedProductBarcode = newsRelatedProductBarcode;
            NewsTitle = newsTitle;
            NewsPublishedDate = newsPublishedDate;
            NewsContent = newsContent;
            NewsStatus = newsStatus;
        }

        public uint GetIdEntity() => NewsId;
    }
}

