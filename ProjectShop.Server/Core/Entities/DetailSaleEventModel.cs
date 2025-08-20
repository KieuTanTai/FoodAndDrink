// File: DetailSaleEvent.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailSaleEventModel : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailSaleEventId { get; set; }

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId { get; set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; set; } = string.Empty;

        // Corresponds to 'discount_type' (ENUM)
        public EDiscountType DiscountType { get; set; }

        // Corresponds to 'discount_percent' (DECIMAL(5,2))
        public decimal DiscountPercent { get; set; }

        // Corresponds to 'discount_amount' (DECIMAL(10,2))
        public decimal DiscountAmount { get; set; }

        // Corresponds to 'max_discount_price' (DECIMAL(10,2))
        public decimal MaxDiscountPrice { get; set; }

        // Corresponds to 'min_price_to_use' (DECIMAL(10,2))
        public decimal MinPriceToUse { get; set; }

        // Navigation properties
        public SaleEventModel SaleEvent { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
        // End of navigation properties

        public DetailSaleEventModel(uint detailSaleEventId, uint saleEventId, string productBarcode, EDiscountType discountType, decimal discountPercent, decimal discountAmount, decimal maxDiscountPrice, decimal minPriceToUse)
        {
            DetailSaleEventId = detailSaleEventId;
            SaleEventId = saleEventId;
            ProductBarcode = productBarcode;
            DiscountType = discountType;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;
            MaxDiscountPrice = maxDiscountPrice;
            MinPriceToUse = minPriceToUse;
        }

        public DetailSaleEventModel() { }

        public uint GetIdEntity() => DetailSaleEventId;
    }
}

