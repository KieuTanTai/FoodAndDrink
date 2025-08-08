// File: NewsImage.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class NewsImage : IGetIdEntity<uint>
    {
        // Corresponds to 'news_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint NewsImageId { get; private set; }

        // Corresponds to 'news_id' (INT UNSIGNED)
        public uint NewsId { get; private set; }
        public News News { get; private set; } = null!;

        // Corresponds to 'news_image_url' (VARCHAR(255))
        public string NewsImageUrl { get; private set; }

        public NewsImage(uint newsImageId, uint newsId, string newsImageUrl)
        {
            NewsImageId = newsImageId;
            NewsId = newsId;
            NewsImageUrl = newsImageUrl;
        }

        public uint GetIdEntity() => NewsImageId;
    }
}

