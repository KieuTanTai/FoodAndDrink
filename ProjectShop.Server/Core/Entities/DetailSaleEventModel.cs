// File: DetailSaleEvent.cs
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailSaleEventModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _detailSaleEventId;
        private uint _saleEventId;
        private string _productBarcode = string.Empty;
        private EDiscountType _discountType;
        private decimal _discountPercent;
        private decimal _discountAmount;
        private decimal _maxDiscountPrice;
        private decimal _minPriceToUse;

        // Corresponds to 'detail_sale_event_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailSaleEventId
        {
            get => _detailSaleEventId;
            set => _detailSaleEventId = value;
        }

        // Corresponds to 'sale_event_id' (INT UNSIGNED)
        public uint SaleEventId
        {
            get => _saleEventId;
            set => _saleEventId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'discount_type' (ENUM)
        public EDiscountType DiscountType
        {
            get => _discountType;
            set => _discountType = value;
        }

        // Corresponds to 'discount_percent' (DECIMAL(5,2))
        public decimal DiscountPercent
        {
            get => _discountPercent;
            set => _discountPercent = value;
        }

        // Corresponds to 'discount_amount' (DECIMAL(10,2))
        public decimal DiscountAmount
        {
            get => _discountAmount;
            set => _discountAmount = value;
        }

        // Corresponds to 'max_discount_price' (DECIMAL(10,2))
        public decimal MaxDiscountPrice
        {
            get => _maxDiscountPrice;
            set => _maxDiscountPrice = value;
        }

        // Corresponds to 'min_price_to_use' (DECIMAL(10,2))
        public decimal MinPriceToUse
        {
            get => _minPriceToUse;
            set => _minPriceToUse = value;
        }

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

