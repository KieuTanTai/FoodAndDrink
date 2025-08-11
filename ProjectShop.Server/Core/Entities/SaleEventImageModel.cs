using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SaleEventImageModel : IGetIdEntity<uint>
    {
        // Corresponds to 'sale_event_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SaleEventImageId { get; private set; }
        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId { get; private set; }
        // Corresponds to 'image_url' (VARCHAR(255))
        public string SaleEventImageUrl { get; private set; } = string.Empty;
        // Corresponds to 'image_priority' (TINYINT UNSIGNED)
        public byte SaleEventImagePriority { get; private set; }
        // Corresponds to 'image_created_date' (DATETIME)
        public DateTime SaleEventImageCreatedDate { get; private set; } = DateTime.UtcNow;

        // Corresponds to 'sale_event_image_Last_updated_date' (DATETIME)
        public DateTime SaleEventImageLastUpdatedDate { get; private set; } = DateTime.UtcNow;

        // Navigation properties
        public SaleEventModel SaleEvent { get; private set; } = null!;
        public SaleEventImageModel(uint saleEventImageId, uint saleEventId, string imageUrl, byte imagePriority)
        {
            SaleEventImageId = saleEventImageId;
            SaleEventId = saleEventId;
            SaleEventImageUrl = imageUrl;
            SaleEventImagePriority = imagePriority;
        }
        public uint GetIdEntity() => SaleEventImageId;
    }
}
