using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailSaleEventDAO : BaseNoneUpdateDAO<DetailSaleEventModel>, IDetailSaleEventDAO<DetailSaleEventModel>
    {
        public DetailSaleEventDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_sale_event", "detail_sale_event_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (sale_event_id, product_barcode, discount_type, discount_percent, discount_amount, max_discount_price, min_price_to_use) 
                            VALUES (@SaleEventId, @ProductBarcode, @DiscountType, @DiscountPercent, @DiscountAmount, @MaxDiscountPrice, @MinPriceToUse;";
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, int? maxGetCount) where TEnum : Enum
        {
            if (tEnum is not EDiscountType discountType)
                throw new ArgumentException("Invalid enum type provided.");
            return await GetByInputAsync(discountType.ToString(), "discount_type", maxGetCount);
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetByDiscountAmountAsync<TCompareType>(decimal discountAmount, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(discountAmount, type, "discount_amount", maxGetCount);
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetByDiscountPercentAsync<TCompareType>(decimal discountPercent, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(discountPercent, type, "discount_percent", maxGetCount);
        }

        public async Task<DetailSaleEventModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
        {
            if (tEnum is not EDiscountType discountType)
                throw new ArgumentException("Invalid enum type provided.");
            return await GetSingleDataAsync(discountType.ToString(), "discount_type");
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetByMaxDiscountPriceAsync<TCompareType>(decimal maxDiscountPrice, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(maxDiscountPrice, type, "max_discount_price", maxGetCount);
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetByMinPriceToUseAsync<TCompareType>(decimal minDiscountPrice, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDecimalAsync(minDiscountPrice, type, "min_price_to_use", maxGetCount);
        }

        public async Task<IEnumerable<DetailSaleEventModel>> GetByProductBarcodeAsync(string productBarcode, int? maxGetCount) 
            => await GetByInputAsync(productBarcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetByProductBarcodesAsync(IEnumerable<string> productBarcodes, int? maxGetCount) 
            => await GetByInputsAsync(productBarcodes, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetByRangeDiscountAmountAsync(decimal minDiscountAmount, decimal maxDiscountAmount, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minDiscountAmount, maxDiscountAmount, "discount_amount", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetByRangeDiscountPercentAsync(decimal minDiscountPercent, decimal maxDiscountPercent, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minDiscountPercent, maxDiscountPercent, "discount_percent", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetByRangeMaxDiscountPriceAsync(decimal minDiscountPrice, decimal maxDiscountPrice, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minDiscountPrice, maxDiscountPrice, "max_discount_price", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetByRangeMinPriceToUseAsync(decimal minDiscountPrice, decimal maxDiscountPrice, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minDiscountPrice, maxDiscountPrice, "min_price_to_use", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetBySaleEventIdAsync(uint saleEventId, int? maxGetCount) 
            => await GetByInputAsync(saleEventId.ToString(), "sale_event_id", maxGetCount);

        public async Task<IEnumerable<DetailSaleEventModel>> GetBySaleEventIdsAsync(IEnumerable<uint> saleEventIds, int? maxGetCount) 
            => await GetByInputsAsync(saleEventIds.Select(id => id.ToString()), "sale_event_id", maxGetCount);
    }
}
