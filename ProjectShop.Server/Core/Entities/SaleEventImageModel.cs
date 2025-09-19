using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SaleEventImageModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _saleEventImageId;
        private uint _saleEventId;
        private string _saleEventImageUrl = string.Empty;
        private byte _saleEventImagePriority;
        private DateTime _saleEventImageCreatedDate = DateTime.UtcNow;
        private DateTime _saleEventImageLastUpdatedDate = DateTime.UtcNow;

        // Corresponds to 'sale_event_image_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SaleEventImageId
        {
            get => _saleEventImageId;
            set => _saleEventImageId = value;
        }

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId
        {
            get => _saleEventId;
            set => _saleEventId = value;
        }

        // Corresponds to 'image_url' (VARCHAR(255))
        public string SaleEventImageUrl
        {
            get => _saleEventImageUrl;
            set => _saleEventImageUrl = value ?? string.Empty;
        }

        // Corresponds to 'image_priority' (TINYINT UNSIGNED)
        public byte SaleEventImagePriority
        {
            get => _saleEventImagePriority;
            set => _saleEventImagePriority = value;
        }

        // Corresponds to 'image_created_date' (DATETIME)
        public DateTime SaleEventImageCreatedDate
        {
            get => _saleEventImageCreatedDate;
            set => _saleEventImageCreatedDate = value;
        }

        // Corresponds to 'sale_event_image_Last_updated_date' (DATETIME)
        public DateTime SaleEventImageLastUpdatedDate
        {
            get => _saleEventImageLastUpdatedDate;
            set => _saleEventImageLastUpdatedDate = value;
        }

        // Navigation properties
        public SaleEventModel SaleEvent { get; set; } = null!;
        // End of navigation properties

        public SaleEventImageModel(uint saleEventImageId, uint saleEventId, string imageUrl, byte imagePriority)
        {
            SaleEventImageId = saleEventImageId;
            SaleEventId = saleEventId;
            SaleEventImageUrl = imageUrl;
            SaleEventImagePriority = imagePriority;
        }

        public SaleEventImageModel()
        {
        }

        public uint GetIdEntity() => SaleEventImageId;
    }
}
