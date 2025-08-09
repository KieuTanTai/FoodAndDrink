// File: DetailSaleEvent.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailSaleEventModel : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailSaleEventId { get; private set; }

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }

        // Corresponds to 'discount_type' (ENUM)
        public string DiscountType { get; private set; }

        // Corresponds to 'discount_percent' (DECIMAL(5,2))
        public decimal DiscountPercent { get; private set; }

        // Corresponds to 'discount_amount' (DECIMAL(10,2))
        public decimal DiscountAmount { get; private set; }

        // Corresponds to 'max_discount_price' (DECIMAL(10,2))
        public decimal MaxDiscountPrice { get; private set; }

        // Corresponds to 'min_price_to_use' (DECIMAL(10,2))
        public decimal MinPriceToUse { get; private set; }

        // Navigation properties
        public SaleEventModel SaleEvent { get; private set; } = null!;
        public ProductModel Product { get; private set; } = null!;

        public DetailSaleEventModel(uint detailSaleEventId, uint saleEventId, string productBarcode, string discountType, decimal discountPercent, decimal discountAmount, decimal maxDiscountPrice, decimal minPriceToUse)
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

        public uint GetIdEntity() => DetailSaleEventId;
    }
}

